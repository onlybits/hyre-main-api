// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases.Candidates;

public sealed class FindCandidateUseCaseTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly JobsRepositoryManager _repository;
	private readonly FindCandidateUseCase _sut;

	public FindCandidateUseCaseTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();

		var logger = Substitute.For<ILoggerManager>();
		var publisherEndpoint = Substitute.For<IPublishEndpoint>();
		_repository = new JobsRepositoryManager(_context, publisherEndpoint, logger);
		_sut = new FindCandidateUseCase(_repository, logger);
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

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldReturnCandidate))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenValidRequest_ShouldReturnCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });


		var request = new FindCandidateRequest(jobOpportunity.Id, candidate.Id, false);

		// Act
		await SeedDatabaseAsync(_context, false, jobOpportunity, new[] { candidate });
		var response = await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().Be(candidate.Id);
		_ = response.Name.Should().Be(candidate.Name);
		_ = response.Email.Should().Be(candidate.Email);
		_ = response.Document.Should().Be(candidate.Document);
		_ = response.DateOfBirth.Should().Be(candidate.DateOfBirth);
		_ = response.Seniority.Should().Be(candidate.Seniority);
		_ = response.Disability.Should().Be(candidate.Disability);
		_ = response.Gender.Should().Be(candidate.Gender);
		_ = response.PhoneNumber.Should().Be(candidate.PhoneNumber);
		_ = response.Address.Should().Be(candidate.Address);
		_ = response.Educations.Should().BeEquivalentTo(candidate.Educations);
		_ = response.Experiences.Should().BeEquivalentTo(candidate.Experiences);
		_ = response.SocialNetwork.Should().Be(candidate.SocialNetwork);
		_ = response.Languages.Should().BeEquivalentTo(candidate.Languages);
		_ = response.CreatedAt.Value.Should().BeCloseTo(candidate.CreatedAt.Value, TimeSpan.FromSeconds(2));
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidJobOpportunityId_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		var request = new FindCandidateRequest(jobOpportunity.Id, candidate.Id, false);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<JobOpportunityNotFoundException>();
	}

	[Fact(DisplayName = nameof(Handle_WhenGivenInvalidCandidateId_ShouldThrowException))]
	[Trait(UseCasesTraits.Name, UseCasesTraits.Value)]
	public async Task Handle_WhenGivenInvalidCandidateId_ShouldThrowException()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();

		_repository.JobOpportunity.Create(jobOpportunity);
		await _repository.CommitChangesAsync(CancellationToken.None);

		var request = new FindCandidateRequest(jobOpportunity.Id, Guid.NewGuid(), false);

		// Act
		var act = async () => await _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = await act
			.Should()
			.ThrowExactlyAsync<CandidateNotFoundException>();
	}
}