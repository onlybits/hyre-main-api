// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="CreateJobOpportunityUseCase" /> class.
/// </summary>
public sealed class CreateJobOpportunityUseCaseTests : CreateJobOpportunityUseCaseTestsFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly CreateJobOpportunityUseCase _sut;

	public CreateJobOpportunityUseCaseTests() => _sut = new CreateJobOpportunityUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidInput_ShouldCreateJobOpportunity))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenGivenValidInput_ShouldCreateJobOpportunity()
	{
		// Arrange
		var request = GenerateValidRequest();

		// Act
		var result = await _sut.Handle(request, CancellationToken.None);

		// Asser
		_ = result.Should().NotBeNull();

		_logger.Received(1).LogInfo(
			Arg.Is("Job opportunity: {JobOpportunity} created successfully."),
			Arg.Any<object?[]>());
	}

	[Fact(DisplayName = nameof(Handle_WhenThrownException_ShouldLogErrorMessages))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public async Task Handle_WhenThrownException_ShouldLogErrorMessages()
	{
		// Arrange
		var request = GenerateValidRequest();
		_repository.SaveAsync(CancellationToken.None).ThrowsAsync(new ArgumentException(""));

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Asser
		await act.Should().ThrowAsync<ArgumentException>();

		_logger.Received(1).LogError(
			Arg.Is("Error creating job opportunity: {JobOpportunity}."),
			Arg.Any<object?[]>());
	}
}