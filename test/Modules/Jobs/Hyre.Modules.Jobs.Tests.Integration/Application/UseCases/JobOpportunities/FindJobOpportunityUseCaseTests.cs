// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Find;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.JobOpportunities;

/// <summary>
///   Integration tests for the <see cref="FindJobOpportunityUseCase" />.
/// </summary>
public sealed class FindJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture
{
	private readonly CancellationToken _cancellationToken = CancellationToken.None;
	private readonly FindJobOpportunityUseCase _sut;

	public FindJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		var context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var repository = new JobsRepositoryManager(context);
		_sut = new FindJobOpportunityUseCase(repository, logger);
	}

	[Fact(DisplayName = nameof(Handle_WhenCalledWithValidId_ShouldReturnJobOpportunity))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalledWithValidId_ShouldReturnJobOpportunity()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunity = jobOpportunities[0];

		var request = new FindJobOpportunityRequest(jobOpportunity.Id, false);

		// Act
		await SeedDatabase(jobOpportunities);
		var result = await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().Be(jobOpportunity.Id);
		_ = result.Name.Should().Be(jobOpportunity.Name);
		_ = result.Description.Should().Be(jobOpportunity.Description);
		_ = result.Location.Should().Be(jobOpportunity.Location);
		_ = result.Contract.Should().Be(jobOpportunity.Contract);
		_ = result.Requirements.Should().BeEquivalentTo(jobOpportunity.Requirements);
	}

	[Fact(DisplayName = nameof(Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalledWithInvalidId_ShouldThrowJobOpportunityNotFoundException()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var request = new FindJobOpportunityRequest(Guid.NewGuid(), false);

		// Act
		await SeedDatabase(jobOpportunities);
		var act = async () => await _sut.Handle(request, _cancellationToken);

		// Assert
		_ = act.Should()
			.ThrowAsync<JobOpportunityNotFoundException>()
			.WithMessage(JobOpportunityErrorMessages.NotFound);
	}
}