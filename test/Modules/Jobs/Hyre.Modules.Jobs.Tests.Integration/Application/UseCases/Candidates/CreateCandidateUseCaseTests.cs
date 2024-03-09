// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

/// <summary>
///   Integration tests for the <see cref="CreateCandidateUseCase" />.
/// </summary>
public sealed class CreateCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly CreateCandidateUseCase _sut;

	public CreateCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();

		var repository = new JobsRepositoryManager(_context);
		var logger = Substitute.For<ILoggerManager>();
		_sut = new CreateCandidateUseCase(repository, logger);
	}

	/// <summary>
	///   Runs before the tests execution.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after the test execution.
	/// </summary>
	public async Task DisposeAsync()
	{
		_ = await _context.JobOpportunities.ExecuteDeleteAsync();
		_ = await _context.Candidates.ExecuteDeleteAsync();
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldCreateCandidate))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenValidRequest_ShouldCreateCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var createCandidateInput = new CreateCandidateInput(GenerateValidCandidateName());
		var createCandidateRequest = new CreateCandidateRequest(jobOpportunity.Id, createCandidateInput, false);

		// Act
		await SeedDatabaseAsync(jobOpportunity);
		var response = await _sut.Handle(createCandidateRequest, CancellationToken.None);

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().NotBe(default);
		_ = response.Name.Should().Be(createCandidateInput.Name);
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException()
	{
		// Arrange
		var createCandidateInput = new CreateCandidateInput(GenerateValidCandidateName());
		var createCandidateRequest = new CreateCandidateRequest(Guid.NewGuid(), createCandidateInput, false);

		// Act
		var act = async () => await _sut.Handle(createCandidateRequest, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();
	}
}