// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Repositories;

#endregion

namespace Hyre.Modules.Jobs.Core.Repositories;

/// <summary>
///   This is the main repository container for the Jobs module.
/// </summary>
public interface IJobsRepositoryManager : IRepositoryManager
{
	/// <summary>
	///   The candidate repository.
	/// </summary>
	ICandidateRepository Candidate { get; }

	/// <summary>
	///   The job opportunity repository.
	/// </summary>
	IJobOpportunityRepository JobOpportunity { get; }
}