// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;
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
///   Unit tests for the <see cref="FindCandidateUseCase" /> class.
/// </summary>
public sealed class FindCandidateUseCaseTests : CandidateBaseFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly FindCandidateUseCase _sut;

	public FindCandidateUseCaseTests() => _sut = new FindCandidateUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldReturnCandidate))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenValidRequest_ShouldReturnCandidate()
	{
		// Arrange
		var request = GenerateFindCandidateRequest();
		var candidate = GenerateValidCandidate();

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(true);

		_ = _repository
			.Candidate
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CandidateId>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
			.Returns(candidate);

		// Act
		var result = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(candidate.ToResponse());
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var request = GenerateFindCandidateRequest();

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
		var request = GenerateFindCandidateRequest();

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(true);

		_ = _repository
			.Candidate
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CandidateId>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
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