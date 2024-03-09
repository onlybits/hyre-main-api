// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Jobs.Infrastructure.Persistence;
using Hyre.Modules.Jobs.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Infrastructure.Persistence;

/// <summary>
///   Integration tests for the <see cref="CandidateRepository" />.
/// </summary>
public sealed class CandidateRepositoryTests : CandidateBaseFixture, IAsyncLifetime
{
	private readonly JobsRepositoryContext _context;
	private readonly CandidateRepository _sut;

	public CandidateRepositoryTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		_sut = new CandidateRepository(_context);
	}

	public Task InitializeAsync() => Task.CompletedTask;

	public async Task DisposeAsync()
	{
		_ = await _context.JobOpportunities.ExecuteDeleteAsync();
		_ = await _context.Candidates.ExecuteDeleteAsync();
	}

	[Theory(DisplayName = nameof(ListAsync_WhenCandidatesExists_ShouldReturnCandidates))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	[InlineData(5, 1, 5, 5, 1, false, false)]
	[InlineData(10, 1, 5, 5, 2, false, true)]
	[InlineData(10, 2, 5, 5, 2, true, false)]
	public async Task ListAsync_WhenCandidatesExists_ShouldReturnCandidates(
		int generate,
		int pageNumber,
		int pageSize,
		int quantityExpected,
		int totalPages,
		bool hasPrevious,
		bool hasNext)
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var parameters = new CandidateParameters
		{
			PageSize = pageSize,
			PageNumber = pageNumber
		};
		var candidates = GenerateCandidatesWithJobOpportunity(generate, jobOpportunity.Id);

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		await SeedDatabaseWithCandidates(candidates);
		var result = await _sut.ListAsync(jobOpportunity.Id, parameters, CancellationToken.None);
		var items = result.Select(c => c).ToList();
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

	[Fact(DisplayName = nameof(FindByIdAsync_WhenCandidateExists_ShouldReturnCandidate))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task FindByIdAsync_WhenCandidateExists_ShouldReturnCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();

		var candidates = GenerateCandidatesWithJobOpportunity(5, jobOpportunity.Id);
		var candidate = candidates.First();

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		await SeedDatabaseWithCandidates(candidates);
		var result = await _sut.FindByIdAsync(candidate.Id, false, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result!.Id.Should().Be(candidate.Id);
		_ = result.Name.Should().Be(candidate.Name);
	}

	[Fact(DisplayName = nameof(FindByIdAsync_WhenCandidateDoesNotExists_ShouldReturnNull))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task FindByIdAsync_WhenCandidateDoesNotExists_ShouldReturnNull()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidates = GenerateCandidatesWithJobOpportunity(5, jobOpportunity.Id);

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		await SeedDatabaseWithCandidates(candidates);
		var result = await _sut.FindByIdAsync(CandidateId.New(), false, CancellationToken.None);

		// Assert
		_ = result.Should().BeNull();
	}

	[Fact(DisplayName = nameof(Create_WhenCandidateIsValid_ShouldCreateCandidate))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Create_WhenCandidateIsValid_ShouldCreateCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidates = GenerateCandidatesWithJobOpportunity(1, jobOpportunity.Id);
		var candidate = candidates.First();

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		_sut.Create(candidate);
		_ = await _context.SaveChangesAsync();

		var result = await _sut.FindByIdAsync(candidate.Id, false, CancellationToken.None);

		// Assert
		_ = _context.Candidates.Should().Contain(candidate);
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(candidate, opt => opt
			.Excluding(x => x.Events)
			.Excluding(x => x.CreatedAt));
	}

	[Fact(DisplayName = nameof(Update_WhenCandidateIsValid_ShouldUpdateCandidate))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Update_WhenCandidateIsValid_ShouldUpdateCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidates = GenerateCandidatesWithJobOpportunity(3, jobOpportunity.Id);
		var candidate = candidates.First();
		var newCandidate = GenerateValidCandidates(1).FirstOrDefault();

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		await SeedDatabaseWithCandidates(candidates);

		candidate.UpdateName(newCandidate!.Name);
		_sut.Update(candidate);
		_ = await _context.SaveChangesAsync();

		var result = await _sut.FindByIdAsync(candidate.Id, false, CancellationToken.None);

		// Assert
		_ = _context.Candidates.Should().Contain(candidate);
		_ = result.Should().NotBeNull();
		_ = result.Should().BeEquivalentTo(candidate, opt => opt
			.Excluding(x => x.Id)
			.Excluding(x => x.Events)
			.Excluding(x => x.CreatedAt));
	}

	[Fact(DisplayName = nameof(Delete_WhenCandidateIsValid_ShouldDeleteCandidate))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Delete_WhenCandidateIsValid_ShouldDeleteCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateValidJobOpportunity();
		var candidates = GenerateCandidatesWithJobOpportunity(3, jobOpportunity.Id);
		var candidate = candidates.First();

		// Act
		await SeedDatabaseWithJobOpportunity(jobOpportunity);
		await SeedDatabaseWithCandidates(candidates);

		_sut.Delete(candidate);
		_ = await _context.SaveChangesAsync();

		var result = await _sut.FindByIdAsync(candidate.Id, false, CancellationToken.None);

		// Assert
		_ = _context.Candidates.Should().NotContain(candidate);
		_ = result.Should().BeNull();
	}
}