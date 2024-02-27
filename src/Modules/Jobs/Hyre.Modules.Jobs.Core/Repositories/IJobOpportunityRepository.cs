// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Shared.Abstractions.Repositories;

#endregion

namespace Hyre.Modules.Jobs.Core.Repositories;

/// <summary>
///   This is the repository contract for the job opportunity entity.
/// </summary>
public interface IJobOpportunityRepository : IRepositoryBase<JobOpportunity>
{
	/// <summary>
	///   This method is responsible for creating a new job opportunity.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be created.</param>
	void Create(JobOpportunity jobOpportunity);
}