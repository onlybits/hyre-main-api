// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
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

		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		var repository = new JobsRepositoryManager(_context, publisherEndpoint, logger);
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
		var createCandidateInput = GenerateCreateCandidateInput();
		var createCandidateRequest = new CreateCandidateRequest(jobOpportunity.Id, createCandidateInput, false);

		// Act
		await SeedDatabaseAsync(jobOpportunity);
		var response = await _sut.Handle(createCandidateRequest, CancellationToken.None);

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().NotBe(default);
		_ = response.Name.Should().Be(createCandidateInput.Name);
		_ = response.Email.Should().Be(createCandidateInput.Email);
		_ = response.Document.Should().Be(createCandidateInput.Document);
		_ = response.DateOfBirth.Should().Be(createCandidateInput.DateOfBirth);
		_ = response.Seniority.Should().Be(createCandidateInput.Seniority);
		_ = response.Disability.Should().Be(createCandidateInput.Disability);
		_ = response.Gender.Should().Be(createCandidateInput.Gender);
		_ = response.PhoneNumber.Should().Be(createCandidateInput.PhoneNumber);
		_ = response.Address.Should().Be(createCandidateInput.Address);
		_ = response.Educations.Should().BeEquivalentTo(createCandidateInput.Educations);
		_ = response.Experiences.Should().BeEquivalentTo(createCandidateInput.Experiences);
		_ = response.SocialNetwork.Should().BeEquivalentTo(createCandidateInput.SocialNetwork);
		_ = response.Languages.Should().BeEquivalentTo(createCandidateInput.Languages);
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException()
	{
		// Arrange
		var createCandidateInput = GenerateCreateCandidateInput();
		var createCandidateRequest = new CreateCandidateRequest(Guid.NewGuid(), createCandidateInput, false);

		// Act
		var act = async () => await _sut.Handle(createCandidateRequest, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenCandidateWithEmailInDatabase_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenCandidateWithEmailInDatabase_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		var createCandidateInput = GenerateCreateCandidateInput();
		var createCandidateRequest = new CreateCandidateRequest(jobOpportunity.Id, createCandidateInput, false);

		// Act
		await SeedDatabaseAsync(jobOpportunity, new[] { candidate });

		var act = async () => await _sut.Handle(createCandidateRequest, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<CandidateAlreadyExistsByEmailException>();
	}
}