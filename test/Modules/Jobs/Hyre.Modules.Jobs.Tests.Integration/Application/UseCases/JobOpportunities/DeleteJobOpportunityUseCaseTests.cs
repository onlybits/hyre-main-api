// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;
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
///   Integration tests for the <see cref="DeleteJobOpportunityUseCase" />.
/// </summary>
public sealed class DeleteJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture
{
	private readonly CancellationToken _cancellationToken = CancellationToken.None;
	private readonly IJobsRepositoryManager _repository;

	private readonly DeleteJobOpportunityUseCase _sut;

	public DeleteJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		var context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		_repository = new JobsRepositoryManager(context, publisherEndpoint, logger);
		_sut = new DeleteJobOpportunityUseCase(_repository, logger);
	}

	[Fact(DisplayName = nameof(Handle_WhenCalledWithValidId_ShouldDeleteJobOpportunity))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalledWithValidId_ShouldDeleteJobOpportunity()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunity = jobOpportunities[0];

		var request = new DeleteJobOpportunityRequest(jobOpportunity.Id, false);

		// Act
		await SeedDatabase(jobOpportunities);
		await _sut.Handle(request, _cancellationToken);
		var result = await _repository.JobOpportunity.FindByIdAsync(jobOpportunity.Id, false, false, _cancellationToken);

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact(DisplayName = nameof(Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var request = new DeleteJobOpportunityRequest(Guid.NewGuid(), false);

		// Act
		await SeedDatabase(jobOpportunities);
		var act = async () => await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = act.Should()
			.ThrowAsync<JobOpportunityNotFoundException>()
			.WithMessage(JobOpportunityErrorMessages.NotFound);
	}
}