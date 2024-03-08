// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="DeleteJobOpportunityUseCase" /> class.
/// </summary>
public sealed class DeleteJobOpportunityUseCaseTests : DeleteJobOpportunityUseCaseTestsFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly DeleteJobOpportunityUseCase _sut;

	public DeleteJobOpportunityUseCaseTests() => _sut = new DeleteJobOpportunityUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidArguments_ShouldDeleteTheJobOpportunity))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenValidArguments_ShouldDeleteTheJobOpportunity()
	{
		// Arrange
		var request = GenerateValidRequest();
		var jobOpportunity = GenerateValidJobOpportunity();
		_ = _repository.JobOpportunity.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), CancellationToken.None)
			.Returns(jobOpportunity);

		// Act
		await _sut.Handle(request, CancellationToken.None);

		// Assert
		_repository.JobOpportunity.Received(1).Delete(Arg.Any<JobOpportunity>());

		_logger.Received(1).LogInfo(
			Arg.Is("Job opportunity with id {Id} was deleted."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenInvalidId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var request = GenerateValidRequest();
		_repository.JobOpportunity.FindByIdAsync(Arg.Any<JobOpportunityId>(), Arg.Any<bool>(), CancellationToken.None).ReturnsNull();

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);
		// Assert
		_ = await act.Should().ThrowAsync<JobOpportunityNotFoundException>()
			.WithMessage(JobOpportunityErrorMessages.NotFound);

		_logger.Received(1).LogError(
			Arg.Is("Job opportunity with id {Id} was not found."),
			Arg.Any<object?[]>());
	}
}