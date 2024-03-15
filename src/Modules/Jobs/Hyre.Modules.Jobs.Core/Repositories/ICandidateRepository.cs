// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
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
	/// <param name="parameters">The parameters to be used in the listing.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns a paged list of candidates.</returns>
	Task<PagedList<Candidate>> ListAsync(
		CandidateParameters parameters,
		CancellationToken cancellationToken = default);

	/// <summary>
	///   This method is responsible for finding a candidate by its id.
	/// </summary>
	/// <param name="candidateId">The candidate id.</param>
	/// <param name="trackChanges">Should EF Core keep track of changes in the candidate entity.</param>
	/// <param name="includeJobOpportunities">Should the job opportunities be included in the candidate entity.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns the candidate found, or null if not found.</returns>
	Task<Candidate?> FindByIdAsync(
		CandidateId candidateId,
		bool trackChanges,
		bool includeJobOpportunities,
		CancellationToken cancellationToken = default);

	/// <summary>
	///   This method is responsible for finding a candidate by its email.
	/// </summary>
	/// <param name="email">The candidate email.</param>
	/// <param name="trackChanges">Should EF Core keep track of changes in the candidate entity.</param>
	/// <param name="includeJobOpportunities">Should the job opportunities be included in the candidate entity.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>Returns the candidate found, or null if not found.</returns>
	Task<Candidate?> FindByEmailAsync(
		CandidateEmail email,
		bool trackChanges,
		bool includeJobOpportunities,
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
}