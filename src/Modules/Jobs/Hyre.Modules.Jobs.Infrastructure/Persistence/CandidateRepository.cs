// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Requests;
using Hyre.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Persistence;

/// <summary>
///   This is the repository for the <see cref="Candidate" /> entity.
/// </summary>
/// <inheritdoc cref="ICandidateRepository" />
internal sealed class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateRepository" /> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public CandidateRepository(DbContext context) : base(context)
	{
	}

	/// <summary>
	///   This method is responsible for listing candidates.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id.</param>
	/// <param name="parameters">The parameters to be used in the listing.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns a paged list of candidates.</returns>
	public async Task<PagedList<Candidate>> ListAsync(
		JobOpportunityId jobOpportunityId,
		CandidateParameters parameters,
		CancellationToken cancellationToken = default)
	{
		var candidates = await FindAll(false)
			.Where(c => c.JobOpportunityId == jobOpportunityId)
			.OrderBy(c => c.Name)
			.Skip((parameters.PageNumber - 1) * parameters.PageSize)
			.Take(parameters.PageSize)
			.ToListAsync(cancellationToken);

		var count = await FindAll(false).Where(c => c.JobOpportunityId == jobOpportunityId).CountAsync(cancellationToken);

		return new PagedList<Candidate>(candidates, count, parameters.PageNumber, parameters.PageSize);
	}

	/// <summary>
	///   This method is responsible for finding a candidate by its id.
	/// </summary>
	/// <param name="candidateId">The candidate id.</param>
	/// <param name="trackChanges">Should EF Core keep track of changes in the candidate entity.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns the candidate found, or null if not found.</returns>
	public async Task<Candidate?> FindByIdAsync(CandidateId candidateId, bool trackChanges, CancellationToken cancellationToken = default) =>
		await FindByCondition(c => c.Id == candidateId, trackChanges).SingleOrDefaultAsync(cancellationToken);

	/// <summary>
	///   This method is responsible for creating a new candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be created.</param>
	public void Create(Candidate candidate) => Insert(candidate);

	/// <summary>
	///   This method is responsible for updating a candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be updated.</param>
	public void Update(Candidate candidate) => Edit(candidate);

	/// <summary>
	///   This method is responsible for deleting a candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be deleted.</param>
	public void Delete(Candidate candidate) => Remove(candidate);
}