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
	public JobOpportunityRepository(DbContext context) : base(context)
	{
	}

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

	public async Task<JobOpportunity?> FindByIdAsync(JobOpportunityId id, bool trackChanges, CancellationToken cancellationToken) =>
		await FindByCondition(jo => jo.Id == id, trackChanges).SingleOrDefaultAsync(cancellationToken);

	public void Create(JobOpportunity jobOpportunity) => Insert(jobOpportunity);

	public void Update(JobOpportunity jobOpportunity) => Edit(jobOpportunity);

	public void Delete(JobOpportunity jobOpportunity) => Remove(jobOpportunity);
}