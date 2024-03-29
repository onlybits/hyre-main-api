// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.JobOpportunities;

/// <summary>
///   Integration tests for the <see cref="UpdateJobOpportunityUseCase" />.
/// </summary>
public sealed class UpdateJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture
{
	private readonly CancellationToken _cancellationToken = CancellationToken.None;
	private readonly IJobsRepositoryManager _repository;
	private readonly UpdateJobOpportunityUseCase _sut;

	public UpdateJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		var context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		_repository = new JobsRepositoryManager(context, publisherEndpoint, logger);
		_sut = new UpdateJobOpportunityUseCase(_repository, logger);
	}

	[Theory(DisplayName = nameof(Handle_WhenCalledWithValidId_ShouldReturnJobOpportunity))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_WhenCalledWithValidId_ShouldReturnJobOpportunity(bool trackChanges)
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunity = jobOpportunities[0];


		var input = new UpdateJobOpportunityInput(
			GenerateValidName(),
			GenerateValidDescription(),
			GenerateValidLocation(),
			GenerateValidContract(),
			GenerateListOfRequirements(2));

		var request = new UpdateJobOpportunityRequest(jobOpportunity.Id, input, trackChanges);

		// Act
		await SeedDatabase(jobOpportunities);
		await _sut.Handle(request, _cancellationToken);
		var result = await _repository.JobOpportunity.FindByIdAsync(jobOpportunity.Id, trackChanges, false, _cancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result!.Id.Should().Be(jobOpportunity.Id);
		_ = result.Name.Should().Be(input.Name);
		_ = result.Description.Should().Be(input.Description);
		_ = result.Location.Should().Be(input.Location);
		_ = result.Contract.Should().Be(input.Contract);
		_ = result.Requirements.Should().BeEquivalentTo(input.Requirements);
	}

	[Fact(DisplayName = nameof(Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var input = new UpdateJobOpportunityInput(
			GenerateValidName(),
			GenerateValidDescription(),
			GenerateValidLocation(),
			GenerateValidContract(),
			GenerateListOfRequirements(7));

		var request = new UpdateJobOpportunityRequest(Guid.NewGuid(), input, false);

		// Act
		await SeedDatabase(jobOpportunities);
		var act = async () => await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = act.Should()
			.ThrowAsync<JobOpportunityNotFoundException>()
			.WithMessage(JobOpportunityErrorMessages.NotFound);
	}
}