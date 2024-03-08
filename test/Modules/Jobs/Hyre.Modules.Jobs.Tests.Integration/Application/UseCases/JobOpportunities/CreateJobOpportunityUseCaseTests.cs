// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.JobOpportunities;

/// <summary>
///   Integration tests for the <see cref="CreateJobOpportunityUseCase" />.
/// </summary>
public sealed class CreateJobOpportunityUseCaseTests : JobOpportunityUseCaseTestsFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly CreateJobOpportunityUseCase _sut;

	public CreateJobOpportunityUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		var logger = Substitute.For<ILoggerManager>();
		var repository = new JobsRepositoryManager(_context);
		_sut = new CreateJobOpportunityUseCase(repository, logger);
	}


	/// <summary>
	///   Runs before the first test in the test class to perform any setup.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after all tests in a test class have run and provides a point for cleanup.
	/// </summary>
	public async Task DisposeAsync() => await _context.JobOpportunities.ExecuteDeleteAsync();

	[Fact(DisplayName = nameof(Handle_WhenCalled_ShouldCreateJobOpportunity))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenCalled_ShouldCreateJobOpportunity()
	{
		// Arrange
		var createJobOpportunityInput = new CreateJobOpportunityInput(
			GenerateValidName(),
			GenerateValidDescription(),
			GenerateValidLocation(),
			GenerateValidContract(),
			GenerateListOfRequirements(5));

		var createJobOpportunityRequest = new CreateJobOpportunityRequest(createJobOpportunityInput);

		// Act
		var result = await _sut.Handle(createJobOpportunityRequest, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Id.Should().NotBeNull();
		_ = result.Name.Should().Be(createJobOpportunityInput.Name);
		_ = result.Description.Should().Be(createJobOpportunityInput.Description);
		_ = result.Location.Should().Be(createJobOpportunityInput.Location);
		_ = result.Contract.Should().Be(createJobOpportunityInput.Contract);
		_ = result.Requirements.Should().BeEquivalentTo(createJobOpportunityInput.Requirements);
	}
}