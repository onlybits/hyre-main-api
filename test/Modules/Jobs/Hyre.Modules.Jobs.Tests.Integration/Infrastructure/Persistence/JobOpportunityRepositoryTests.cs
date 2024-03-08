// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Infrastructure.Persistence;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Infrastructure.Persistence;

/// <summary>
///   Integration tests for the <see cref="JobOpportunityRepository" />.
/// </summary>
public sealed class JobOpportunityRepositoryTests : JobOpportunityRepositoryTestsFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly CancellationToken _ct = CancellationToken.None;
	private readonly JobOpportunityRepository _sut;

	public JobOpportunityRepositoryTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		_sut = new JobOpportunityRepository(_context);
	}

	/// <summary>
	///   Runs before the test execution.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after the test execution.
	/// </summary>
	public async Task DisposeAsync() => await _context.JobOpportunities.ExecuteDeleteAsync(_ct);

	[Theory(DisplayName = nameof(ListAsync_WhenJobOpportunitiesExists_ShouldReturnJobOpportunities))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	[InlineData(5, 1, 5, 5, 1, false, false)]
	[InlineData(10, 1, 5, 5, 2, false, true)]
	[InlineData(10, 2, 5, 5, 2, true, false)]
	public async Task ListAsync_WhenJobOpportunitiesExists_ShouldReturnJobOpportunities(
		int generate,
		int pageNumber,
		int pageSize,
		int quantityExpected,
		int totalPages,
		bool hasPrevious,
		bool hasNext)
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(generate);
		var parameters = new JobOpportunityParameters
		{
			PageSize = pageSize,
			PageNumber = pageNumber
		};

		// Act
		await SeedDatabase(jobOpportunities);
		var result = await _sut.ListAsync(parameters, _ct);
		var items = result.Select(jo => jo).ToList();
		var metadata = result.MetaData;

		// Assert
		_ = result.Should().NotBeNullOrEmpty();
		_ = items.Should().NotBeNullOrEmpty();
		_ = items.Should().HaveCount(quantityExpected);
		_ = metadata.Should().NotBeNull();
		_ = metadata.PageSize.Should().Be(pageSize);
		_ = metadata.CurrentPage.Should().Be(pageNumber);
		_ = metadata.TotalCount.Should().Be(generate);
		_ = metadata.TotalPages.Should().Be(totalPages);
		_ = metadata.HasPrevious.Should().Be(hasPrevious);
		_ = metadata.HasNext.Should().Be(hasNext);
	}

	[Fact(DisplayName = nameof(FindByIdAsync_WhenJobOpportunityExists_ShouldReturnJobOpportunity))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task FindByIdAsync_WhenJobOpportunityExists_ShouldReturnJobOpportunity()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunity = jobOpportunities.First();

		// Act
		await SeedDatabase(jobOpportunities);
		var result = await _sut.FindByIdAsync(jobOpportunity.Id, false, false, _ct);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result!.Should().BeEquivalentTo(jobOpportunity, opt => opt
			.Excluding(x => x.Events)
			.Excluding(x => x.CreatedAt));
	}

	[Fact(DisplayName = nameof(FindByIdAsync_WhenJobOpportunityDoesNotExist_ShouldReturnNull))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task FindByIdAsync_WhenJobOpportunityDoesNotExist_ShouldReturnNull()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		JobOpportunityId id = Guid.NewGuid();

		// Act
		await SeedDatabase(jobOpportunities);
		var result = await _sut.FindByIdAsync(id, false, false, _ct);

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact(DisplayName = nameof(FindByIdAsync_WhenIncludeCandidatesIsTrue_ShouldIncludeCandidates))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task FindByIdAsync_WhenIncludeCandidatesIsTrue_ShouldIncludeCandidates()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunityWithCandidates();
		var list = new List<JobOpportunity> { jobOpportunity };

		// Act
		await SeedDatabase(list);
		var result = await _sut.FindByIdAsync(jobOpportunity.Id, true, true, _ct);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result!.Candidates.Should().NotBeNullOrEmpty();
	}

	[Fact(DisplayName = nameof(Create_WhenGivenValidInputData_ShouldCreateJobOpportunity))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Create_WhenGivenValidInputData_ShouldCreateJobOpportunity()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunity();

		// Act
		_sut.Create(jobOpportunity);
		_ = await _context.SaveChangesAsync(_ct);

		var result = await _sut.FindByIdAsync(jobOpportunity.Id, false, false, _ct);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(jobOpportunity, opt => opt
			.Excluding(x => x.Events)
			.Excluding(x => x.CreatedAt));
	}

	[Fact(DisplayName = nameof(Update_WhenGivenValidInputData_ShouldUpdateJobOpportunity))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Update_WhenGivenValidInputData_ShouldUpdateJobOpportunity()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunity = jobOpportunities.First();
		await SeedDatabase(jobOpportunities);

		var updatedJobOpportunity = GenerateJobOpportunity();

		// Act
		jobOpportunity.UpdateName(updatedJobOpportunity.Name);
		jobOpportunity.UpdateDescription(updatedJobOpportunity.Description);
		jobOpportunity.UpdateContract(updatedJobOpportunity.Contract);
		jobOpportunity.UpdateLocation(updatedJobOpportunity.Location);
		jobOpportunity.UpdateRequirements(updatedJobOpportunity.Requirements);

		_sut.Update(jobOpportunity);
		_ = await _context.SaveChangesAsync(_ct);

		var result = await _sut.FindByIdAsync(jobOpportunity.Id, false, false, _ct);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(updatedJobOpportunity,
			opt => opt
				.Excluding(x => x.Id)
				.Excluding(x => x.Events)
				.Excluding(x => x.CreatedAt)
			// I'm excluding the Id because i'm comparing with a new generated job opportunity.
		);
	}

	[Fact(DisplayName = nameof(Delete_WhenGivenValidJobOpportunity_ShouldDeleteJobOpportunity))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Delete_WhenGivenValidJobOpportunity_ShouldDeleteJobOpportunity()
	{
		// Arrange
		var jobOpportunities = GenerateJobOpportunities(5);
		var jobOpportunitiesToDelete = jobOpportunities.First();

		// Act
		await SeedDatabase(jobOpportunities);
		_sut.Delete(jobOpportunitiesToDelete);
		_ = await _context.SaveChangesAsync(_ct);

		var result = await _sut.FindByIdAsync(jobOpportunitiesToDelete.Id, false, false, _ct);
		var allJobOpportunities = await _context.JobOpportunities.ToListAsync(_ct);

		// Assert
		_ = result.Should().BeNull();
		_ = allJobOpportunities.Should().NotBeNull();
		_ = allJobOpportunities.Should().NotContain(jobOpportunitiesToDelete);
		_ = allJobOpportunities.Should().HaveCount(4);
	}

	/// <summary>
	///   This method is responsible for seeding the database with the given job opportunities.
	/// </summary>
	/// <param name="jobOpportunities">The job opportunities to be seeded.</param>
	private async Task SeedDatabase(IEnumerable<JobOpportunity> jobOpportunities)
	{
		var contextToSeed = CreateRepositoryContext();
		await contextToSeed.JobOpportunities.AddRangeAsync(jobOpportunities, _ct);
		_ = await contextToSeed.SaveChangesAsync(_ct);
	}
}