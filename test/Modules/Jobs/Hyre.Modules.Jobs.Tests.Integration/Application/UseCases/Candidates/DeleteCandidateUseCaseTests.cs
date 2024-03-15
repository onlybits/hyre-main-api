// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Delete;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

/// <summary>
///   Integration tests for the <see cref="DeleteCandidateUseCase" />.
/// </summary>
public sealed class DeleteCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly JobsRepositoryManager _repository;
	private readonly DeleteCandidateUseCase _sut;

	public DeleteCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		_repository = new JobsRepositoryManager(_context, publisherEndpoint, logger);
		_sut = new DeleteCandidateUseCase(_repository, logger);
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

	[Fact(DisplayName = nameof(Handle_WhenGivenValidId_ShouldDeleteCandidate))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenValidId_ShouldDeleteCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });
		var request = new DeleteCandidateRequest(candidate.Id, true);

		// Act
		await SeedDatabaseAsync(_context, true, jobOpportunity, new[] { candidate });

		await _sut.Handle(request, CancellationToken.None);
		var deletedCandidate = await _repository
			.Candidate
			.FindByIdAsync(candidate.Id, false, false, CancellationToken.None);

		// Assert
		_ = deletedCandidate.Should().BeNull();
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidCandidateId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidCandidateId_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });
		var request = new DeleteCandidateRequest(Guid.NewGuid(), false);

		// Act
		await SeedDatabaseAsync(_context, false, jobOpportunity, new[] { candidate });
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<CandidateNotFoundException>();
	}
}