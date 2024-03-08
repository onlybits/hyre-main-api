// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Requests;
using Hyre.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Persistence;

/// <summary>
///   This is the repository for the <see cref="JobOpportunity" /> entity.
/// </summary>
/// <inheritdoc cref="IJobOpportunityRepository" />
internal sealed class JobOpportunityRepository : RepositoryBase<JobOpportunity>, IJobOpportunityRepository
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityRepository" /> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public JobOpportunityRepository(DbContext context) : base(context)
	{
	}

	/// <summary>
	///   This method is responsible for listing job opportunities.
	/// </summary>
	/// <param name="parameters">The parameters to be used in the listing.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>It will return a paged list of job opportunities.</returns>
	public async Task<PagedList<JobOpportunity>> ListAsync(JobOpportunityParameters parameters, CancellationToken cancellationToken)
	{
		var jobOpportunities = await FindAll(false)
			.OrderBy(e => e.Name)
			.Skip((parameters.PageNumber - 1) * parameters.PageSize)
			.Take(parameters.PageSize)
			.ToListAsync(cancellationToken);

		var count = await FindAll(false).CountAsync(cancellationToken);

		return new PagedList<JobOpportunity>(jobOpportunities, count, parameters.PageNumber, parameters.PageSize);
	}

	/// <summary>
	///   This method is responsible for finding a job opportunity by its id.
	/// </summary>
	/// <param name="id">The job opportunity id.</param>
	/// <param name="trackChanges">Should EF keep track of the changes.</param>
	/// <param name="cancellationToken">The cancellation token, used to cancel the operation.</param>
	/// <returns>It will return the job opportunity found.</returns>
	public async Task<JobOpportunity?> FindByIdAsync(JobOpportunityId id, bool trackChanges, CancellationToken cancellationToken) =>
		await FindByCondition(jo => jo.Id == id, trackChanges).SingleOrDefaultAsync(cancellationToken);

	/// <summary>
	///   This method is responsible for creating a new job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be created.</param>
	public void Create(JobOpportunity jobOpportunity) => Insert(jobOpportunity);

	/// <summary>
	///   This method is responsible for updating a job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be updated.</param>
	public void Update(JobOpportunity jobOpportunity) => Edit(jobOpportunity);

	/// <summary>
	///   This method is responsible for deleting a job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be deleted.</param>
	public void Delete(JobOpportunity jobOpportunity) => Remove(jobOpportunity);
}