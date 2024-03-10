// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.List;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

/// <summary>
///   Integration tests for the <see cref="ListCandidateUseCase" /> class.
/// </summary>
public sealed class ListCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly ListCandidateUseCase _sut;

	public ListCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();

		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		var repository = new JobsRepositoryManager(_context, publisherEndpoint, logger);
		_sut = new ListCandidateUseCase(repository, logger);
	}

	/// <summary>
	///   Runs before the tests execution.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after the test execution.
	/// </summary>
	public async Task DisposeAsync()
	{
		_ = await _context.JobOpportunities.ExecuteDeleteAsync();
		_ = await _context.Candidates.ExecuteDeleteAsync();
	}

	[Theory(DisplayName = nameof(Handle_WhenCalled_ShouldListCandidates))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(5, 1, 5, 5, 1, false, false)]
	[InlineData(10, 1, 5, 5, 2, false, true)]
	[InlineData(10, 2, 5, 5, 2, true, false)]
	public async Task Handle_WhenCalled_ShouldListCandidates(
		int generate,
		int pageNumber,
		int pageSize,
		int quantityExpected,
		int totalPages,
		bool hasPrevious,
		bool hasNext)
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidates = GenerateCandidatesWithJobOpportunity(generate, jobOpportunity.Id);
		var parameters = new CandidateParameters
		{
			PageSize = pageSize,
			PageNumber = pageNumber
		};

		var request = new ListCandidateRequest(jobOpportunity.Id, false, parameters);

		// Act
		await SeedDatabaseAsync(jobOpportunity, candidates);
		var (metadata, items) = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = items.Should().NotBeNullOrEmpty();
		_ = items.Should().HaveCount(quantityExpected);
		_ = metadata.Should().NotBeNull();
		_ = metadata.PageSize.Should().Be(pageSize);
		_ = metadata.CurrentPage.Should().Be(pageNumber);
		_ = metadata.TotalCount.Should().Be(generate);
		_ = metadata.TotalPages.Should().Be(totalPages);
		_ = metadata.HasPrevious.Should().Be(hasPrevious);
		_ = metadata.HasNext.Should().Be(hasNext);
	}
}