// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Find;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="FindJobOpportunityUseCase" /> class.
/// </summary>
public sealed class FindJobOpportunityUseCaseTests : FindJobOpportunityUseCaseTestsFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly FindJobOpportunityUseCase _sut;

	public FindJobOpportunityUseCaseTests() => _sut = new FindJobOpportunityUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidId_ShouldReturnJobOpportunity))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenValidId_ShouldReturnJobOpportunity()
	{
		// Arrange
		var request = GenerateValidRequest();
		var finalResult = GenerateValidJobOpportunity();
		_ = _repository
			.JobOpportunity
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), Arg.Any<bool>(), CancellationToken.None)
			.Returns(finalResult);

		// Act
		var result = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(finalResult, options => options.ExcludingMissingMembers());

		_logger.Received(1).LogInfo(
			Arg.Is("Job opportunity with id {Id} found successfully."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenInvalidId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var request = GenerateValidRequest();
		_ = _repository
			.JobOpportunity
			.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), Arg.Any<bool>(), CancellationToken.None)
			.ReturnsNull();

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act.Should()
			.ThrowAsync<JobOpportunityNotFoundException>()
			.WithMessage(JobOpportunityErrorMessages.NotFound);

		_logger.Received(1).LogError(
			Arg.Is("Job opportunity with id {Id} was not found."),
			Arg.Any<object?[]>());
	}
}