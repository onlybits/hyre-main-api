// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.Candidates;

/// <summary>
///   Unit tests for the <see cref="UpdateCandidateUseCase" /> class.
/// </summary>
public sealed class UpdateCandidatesUseCaseTests : CandidateBaseFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly UpdateCandidateUseCase _sut;

	public UpdateCandidatesUseCaseTests() => _sut = new UpdateCandidateUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldUpdateCandidate))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenValidRequest_ShouldUpdateCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunity();
		var request = GenerateUpdateCandidateRequest();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(true);

		_ = _repository
			.Candidate
			.FindByIdAsync(Arg.Any<CandidateId>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
			.Returns(candidate);

		// Act
		await _sut.Handle(request, CancellationToken.None);

		// Assert
		_logger.Received(1).LogInfo(
			Arg.Is("Candidate {CandidateId} updated for job opportunity {JobOpportunityId}."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var request = GenerateUpdateCandidateRequest();

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(false);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();

		_logger.Received(1).LogError(
			Arg.Is("Job opportunity with id {JobOpportunityId} not found."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidCandidateId_ShouldThrowCandidateNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenInvalidCandidateId_ShouldThrowCandidateNotFoundException()
	{
		// Arrange
		var request = GenerateUpdateCandidateRequest();

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(true);

		_ = _repository
			.Candidate
			.FindByIdAsync(Arg.Any<CandidateId>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
			.ReturnsNull();

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act.Should()
			.ThrowExactlyAsync<CandidateNotFoundException>();

		_logger.Received(1).LogError(
			Arg.Is("Candidate with id {CandidateId} not found."),
			Arg.Any<object?[]>());
	}
}