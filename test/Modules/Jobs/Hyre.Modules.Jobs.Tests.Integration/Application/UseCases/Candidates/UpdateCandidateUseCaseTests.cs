// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Update;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

/// <summary>
///   Integration tests for the <see cref="UpdateCandidateUseCase" />.
/// </summary>
public sealed class UpdateCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly CancellationToken _cancellationToken = CancellationToken.None;
	private readonly JobsRepositoryContext _context;
	private readonly JobsRepositoryManager _repository;
	private readonly UpdateCandidateUseCase _sut;

	public UpdateCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();

		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		_repository = new JobsRepositoryManager(_context, publisherEndpoint, logger);
		_sut = new UpdateCandidateUseCase(_repository, logger);
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
		_ = await _context.JobOpportunities.ExecuteDeleteAsync(_cancellationToken);
		_ = await _context.Candidates.ExecuteDeleteAsync(_cancellationToken);
		await _context.DisposeAsync();
	}

	[Theory(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldUpdateCandidate))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_WhenGivenValidRequest_ShouldUpdateCandidate(bool trackChanges)
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidateWithJobOpportunity(jobOpportunity.Id);
		var newCandidateName = GenerateCandidateName();

		await SeedDatabaseAsync(jobOpportunity, new[] { candidate });

		var input = new UpdateCandidateInput(newCandidateName);
		var request = new UpdateCandidateRequest(jobOpportunity.Id, candidate.Id, input, trackChanges);

		// Act
		await _sut.Handle(request, _cancellationToken);
		var updatedCandidate = await _repository.Candidate.FindByIdAsync(jobOpportunity.Id, candidate.Id, false, _cancellationToken);

		// Assert
		_ = updatedCandidate.Should().NotBeNull();
		_ = updatedCandidate!.Id.Should().Be(candidate.Id);
		_ = updatedCandidate.Name.Should().Be(newCandidateName);
	}

	[Theory(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException(bool trackChanges)
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidateWithJobOpportunity(jobOpportunity.Id);
		var newCandidateName = GenerateCandidateName();

		_ = await _context.JobOpportunities.AddAsync(jobOpportunity, _cancellationToken);
		_ = await _context.Candidates.AddAsync(candidate, _cancellationToken);
		_ = await _context.SaveChangesAsync(_cancellationToken);

		var input = new UpdateCandidateInput(newCandidateName);
		var request = new UpdateCandidateRequest(Guid.NewGuid(), candidate.Id, input, trackChanges);

		// Act
		var act = async () => await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();
	}

	[Theory(DisplayName = nameof(Handle_WhenGivenInvalidCandidateId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_WhenGivenInvalidCandidateId_ShouldThrowException(bool trackChanges)
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidateWithJobOpportunity(jobOpportunity.Id);
		var newCandidateName = GenerateCandidateName();

		_ = await _context.JobOpportunities.AddAsync(jobOpportunity, _cancellationToken);
		_ = await _context.Candidates.AddAsync(candidate, _cancellationToken);
		_ = await _context.SaveChangesAsync(_cancellationToken);

		var input = new UpdateCandidateInput(newCandidateName);
		var request = new UpdateCandidateRequest(jobOpportunity.Id, Guid.NewGuid(), input, trackChanges);

		// Act
		var act = async () => await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<CandidateNotFoundException>();
	}
}