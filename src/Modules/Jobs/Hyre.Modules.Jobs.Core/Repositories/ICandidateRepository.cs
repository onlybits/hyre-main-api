// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Repositories;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Core.Repositories;

/// <summary>
///   This is the repository contract for the job opportunity entity.
/// </summary>
public interface ICandidateRepository : IRepositoryBase<Candidate>
{
	/// <summary>
	///   This method is responsible for listing candidates.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id.</param>
	/// <param name="parameters">The parameters to be used in the listing.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns a paged list of candidates.</returns>
	Task<PagedList<Candidate>> ListAsync(
		JobOpportunityId jobOpportunityId,
		CandidateParameters parameters,
		CancellationToken cancellationToken = default);

	/// <summary>
	///   This method is responsible for finding a candidate by its id.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id.</param>
	/// <param name="candidateId">The candidate id.</param>
	/// <param name="trackChanges">Should EF Core keep track of changes in the candidate entity.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns the candidate found, or null if not found.</returns>
	Task<Candidate?> FindByIdAsync(
		JobOpportunityId jobOpportunityId,
		CandidateId candidateId,
		bool trackChanges,
		CancellationToken cancellationToken = default);

	/// <summary>
	///   This method is responsible for creating a new candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be created.</param>
	void Create(Candidate candidate);

	/// <summary>
	///   This method is responsible for updating a candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be updated.</param>
	void Update(Candidate candidate);

	/// <summary>
	///   This method is responsible for deleting a candidate.
	/// </summary>
	/// <param name="candidate">The candidate to be deleted.</param>
	void Delete(Candidate candidate);

	#region Database Checks

	/// <summary>
	///   This method is responsible for checking if a candidate exists by arguments.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id.</param>
	/// <param name="email">The candidate email.</param>
	/// <param name="trackChanges">Should EF Core keep track of changes in the candidate entity.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns true if the candidate exists, otherwise false.</returns>
	Task<bool> ExistsAsync(
		JobOpportunityId jobOpportunityId,
		CandidateEmail email,
		bool trackChanges,
		CancellationToken cancellationToken = default);

	#endregion
}