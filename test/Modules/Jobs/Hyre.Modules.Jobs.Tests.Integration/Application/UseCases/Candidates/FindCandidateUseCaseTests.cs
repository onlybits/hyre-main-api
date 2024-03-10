// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

public sealed class FindCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly JobsRepositoryManager _repository;
	private readonly FindCandidateUseCase _sut;

	public FindCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();

		_repository = new JobsRepositoryManager(_context);
		var logger = Substitute.For<ILoggerManager>();
		_sut = new FindCandidateUseCase(_repository, logger);
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

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldReturnCandidate))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenValidRequest_ShouldReturnCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidateWithJobOpportunity(jobOpportunity.Id);


		var request = new FindCandidateRequest(jobOpportunity.Id, candidate.Id, false);

		// Act
		await SeedDatabaseAsync(jobOpportunity, new[] { candidate });
		var response = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().Be(candidate.Id);
		_ = response.Name.Should().Be(candidate.Name);
		_ = response.Email.Should().Be(candidate.Email);
		_ = response.JobOpportunityId.Should().Be(candidate.JobOpportunityId);
		_ = response.CreatedAt.Value.Should().BeCloseTo(candidate.CreatedAt.Value, TimeSpan.FromSeconds(2));
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidateWithJobOpportunity(jobOpportunity.Id);

		var request = new FindCandidateRequest(jobOpportunity.Id, candidate.Id, false);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidCandidateId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidCandidateId_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();

		_repository.JobOpportunity.Create(jobOpportunity);
		await _repository.CommitChangesAsync(CancellationToken.None);

		var request = new FindCandidateRequest(jobOpportunity.Id, Guid.NewGuid(), false);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<CandidateNotFoundException>();
	}
}