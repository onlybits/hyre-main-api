// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.JobOpportunities;

/// <summary>
///   Integration tests for the <see cref="CreateJobOpportunityUseCase" />.
/// </summary>
public sealed class CreateJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture
{
	private readonly CreateJobOpportunityUseCase _sut;

	public CreateJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		var context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var repository = new JobsRepositoryManager(context);
		_sut = new CreateJobOpportunityUseCase(repository, logger);
	}

	[Fact(DisplayName = nameof(Handle_WhenCalled_ShouldCreateJobOpportunity))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalled_ShouldCreateJobOpportunity()
	{
		// Arrange
		var createJobOpportunityInput = new CreateJobOpportunityInput(GenerateJobOpportunityName());
		var createJobOpportunityRequest = new CreateJobOpportunityRequest(createJobOpportunityInput);

		// Act
		var result = await _sut.Handle(createJobOpportunityRequest, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().NotBeNull();
		_ = result.Name.Should().Be(createJobOpportunityInput.Name);
	}
}