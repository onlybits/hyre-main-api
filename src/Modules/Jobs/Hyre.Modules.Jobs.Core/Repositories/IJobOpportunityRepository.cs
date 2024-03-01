// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Repositories;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Core.Repositories;

/// <summary>
///   This is the repository contract for the job opportunity entity.
/// </summary>
public interface IJobOpportunityRepository : IRepositoryBase<JobOpportunity>
{
	/// <summary>
	///   This method is responsible for listing job opportunities.
	/// </summary>
	/// <param name="parameters">The parameters to be used in the listing.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>It will return a paged list of job opportunities.</returns>
	Task<PagedList<JobOpportunity>> ListAsync(JobOpportunityParameters parameters, CancellationToken cancellationToken);

	/// <summary>
	///   This method is responsible for finding a job opportunity by its id.
	/// </summary>
	/// <param name="id">The job opportunity id.</param>
	/// <param name="trackChanges">Should EF keep track of the changes.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>It will return the job opportunity found.</returns>
	Task<JobOpportunity?> FindByIdAsync(JobOpportunityId id, bool trackChanges, CancellationToken cancellationToken);

	/// <summary>
	///   This method is responsible for creating a new job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be created.</param>
	void Create(JobOpportunity jobOpportunity);

	/// <summary>
	///   This method is responsible for updating a job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be updated.</param>
	void Update(JobOpportunity jobOpportunity);
}