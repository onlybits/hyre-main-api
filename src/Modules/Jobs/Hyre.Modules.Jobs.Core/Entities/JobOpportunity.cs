// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Jobs.Core.Entities;

/// <summary>
///   Represents a job opportunity in the system.
/// </summary>
public sealed class JobOpportunity : EntityBase<JobOpportunityId>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <param name="id">The identifier of the job opportunity.</param>
	/// <param name="name">The name of the job opportunity.</param>
	/// <param name="description">The description of the job opportunity.</param>
	private JobOpportunity(
		JobOpportunityId id,
		JobOpportunityName name,
		JobOpportunityDescription description) : base(id)
	{
		Name = name;
		Description = description;
	}

	/// <summary>
	///   Gets or sets the name of the job opportunity.
	/// </summary>
	public JobOpportunityName Name { get; private set; }

	/// <summary>
	///   Gets or sets the description of the job opportunity.
	/// </summary>
	public JobOpportunityDescription Description { get; private set; }

	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <param name="name">The name of the job opportunity.</param>
	/// <param name="description">The description of the job opportunity.</param>
	/// <returns>It will return a new instance of the <see cref="JobOpportunity" /> class.</returns>
	public static JobOpportunity Create(
		JobOpportunityName name,
		JobOpportunityDescription description) => new(JobOpportunityId.New(), name, description);

	#region Update Methods

	/// <summary>
	///   This method updates the name of the job opportunity.
	/// </summary>
	/// <param name="name">The new name of the job opportunity.</param>
	public void UpdateName(JobOpportunityName? name) => Name = name ?? Name;

	/// <summary>
	///   This method updates the description of the job opportunity.
	/// </summary>
	/// <param name="description">The new description of the job opportunity.</param>
	public void UpdateDescription(JobOpportunityDescription? description) => Description = description ?? Description;

	#endregion
}