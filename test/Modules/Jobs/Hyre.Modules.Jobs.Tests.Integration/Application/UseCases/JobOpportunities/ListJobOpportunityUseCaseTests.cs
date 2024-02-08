// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.JobOpportunities;

/// <summary>
///   Integration tests for the <see cref="ListJobOpportunityUseCase" /> class.
/// </summary>
public sealed class ListJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly ListJobOpportunityUseCase _sut;

	public ListJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var repository = new JobsRepositoryManager(_context);
		_sut = new ListJobOpportunityUseCase(repository, logger);
	}

	/// <summary>
	///   Runs before the first test in the test class to perform any setup.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after all tests in a test class have run and provides a point for cleanup.
	/// </summary>
	public async Task DisposeAsync() => await _context.JobOpportunities.ExecuteDeleteAsync();

	[Theory(DisplayName = nameof(Handle_WhenCalled_ShouldListJobOpportunities))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(5, 1, 5, 5, 1, false, false)]
	[InlineData(10, 1, 5, 5, 2, false, true)]
	[InlineData(10, 2, 5, 5, 2, true, false)]
	public async Task Handle_WhenCalled_ShouldListJobOpportunities(
		int generate,
		int pageNumber,
		int pageSize,
		int quantityExpected,
		int totalPages,
		bool hasPrevious,
		bool hasNext)
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(generate);
		var parameters = new JobOpportunityParameters
		{
			PageSize = pageSize,
			PageNumber = pageNumber
		};

		var request = new ListJobOpportunityRequest(parameters);

		// Act
		await SeedDatabase(jobOpportunities);
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