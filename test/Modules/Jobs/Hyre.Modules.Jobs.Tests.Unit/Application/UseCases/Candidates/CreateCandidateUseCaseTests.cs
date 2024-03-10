// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Core.Constants;
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
///   Unit tests for the <see cref="CreateCandidateUseCase" /> class.
/// </summary>
public sealed class CreateCandidateUseCaseTests : CandidateBaseFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly CreateCandidateUseCase _sut;

	public CreateCandidateUseCaseTests() => _sut = new CreateCandidateUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenValidRequest_ShouldCreateCandidate))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenValidRequest_ShouldCreateCandidate()
	{
		// Arrange
		var request = GenerateCreateCandidateRequest();
		var jobOpportunity = GenerateValidJobOpportunity();

		_ = _repository
			.JobOpportunity
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
			.Returns(jobOpportunity);

		// Act
		var result = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();

		_logger.Received(1).LogInfo(
			Arg.Is("Candidate {CandidateId} created for job opportunity {JobOpportunityId}."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenJobOpportunityNotFound_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public void Handle_WhenJobOpportunityNotFound_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var request = GenerateCreateCandidateRequest();
		_ = _repository.JobOpportunity.FindByIdAsync(
				request.JobOpportunityId,
				request.JobOpportunityTrackChanges,
				true,
				CancellationToken.None)
			.ReturnsNull();

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = act.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();

		_logger.Received(1).LogError(
			Arg.Is("Job opportunity with id {JobOpportunityId} not found."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenCandidateWithEmailExists_ShouldThrowCandidateWithEmailExistsException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public void Handle_WhenCandidateWithEmailExists_ShouldThrowCandidateWithEmailExistsException()
	{
		// Arrange
		var request = GenerateCreateCandidateRequest();
		var jobOpportunity = GenerateValidJobOpportunity();

		_ = _repository
			.JobOpportunity
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
			.Returns(jobOpportunity);

		_ = _repository
			.Candidate
			.ExistsAsync(
				Arg.Any<JobOpportunityId>(),
				Arg.Any<CandidateEmail>(),
				Arg.Any<bool>(),
				Arg.Any<CancellationToken>())
			.Returns(true);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = act.Should()
			.ThrowExactlyAsync<CandidateAlreadyExistsByEmailException>()
			.WithMessage(CandidateErrorMessages.AlreadyExistsByEmail);

		_logger.Received(1).LogError(
			Arg.Is("Candidate with email {Email} already exists for job opportunity {JobOpportunityId}."),
			Arg.Any<object?[]>());
	}
}