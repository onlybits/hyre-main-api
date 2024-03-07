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
	///   This constructor is only used by EF Core.
	/// </summary>
	private JobOpportunity() : base(JobOpportunityId.New())
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <param name="id">The identifier of the job opportunity.</param>
	/// <param name="name">The name of the job opportunity.</param>
	/// <param name="description">The description of the job opportunity.</param>
	/// <param name="location">The location of the job opportunity.</param>
	/// <param name="contract">The contract of the job opportunity.</param>
	private JobOpportunity(
		JobOpportunityId id,
		JobOpportunityName name,
		JobOpportunityDescription description,
		JobOpportunityLocation location,
		JobOpportunityContract contract) : base(id)
	{
		Name = name;
		Description = description;
		Location = location;
		Contract = contract;
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
	///   Gets or sets the location of the job opportunity.
	/// </summary>
	public JobOpportunityLocation Location { get; private set; }

	/// <summary>
	///   Gets or sets the contract of the job opportunity.
	/// </summary>
	public JobOpportunityContract Contract { get; private set; }

	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <param name="name">The name of the job opportunity.</param>
	/// <param name="description">The description of the job opportunity.</param>
	/// <param name="location">The location of the job opportunity.</param>
	/// <param name="contract">The contract of the job opportunity.</param>
	/// <returns>It will return a new instance of the <see cref="JobOpportunity" /> class.</returns>
	public static JobOpportunity Create(
		JobOpportunityName name,
		JobOpportunityDescription description,
		JobOpportunityLocation location,
		JobOpportunityContract contract) => new(
		JobOpportunityId.New(),
		name,
		description,
		location,
		contract);

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

	/// <summary>
	///   This method updates the location of the job opportunity.
	/// </summary>
	/// <param name="location">The new location of the job opportunity.</param>
	public void UpdateLocation(JobOpportunityLocation? location) => Location = location ?? Location;

	/// <summary>
	///   This method updates the contract of the job opportunity.
	/// </summary>
	/// <param name="contract">The new contract of the job opportunity.</param>
	public void UpdateContract(JobOpportunityContract? contract) => Contract = contract ?? Contract;

	#endregion
}