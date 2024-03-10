// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Jobs.Core.Entities;

/// <summary>
///   Represents a candidate for a job opportunity in the system.
/// </summary>
public sealed class Candidate : EntityBase<CandidateId>
{
	/// <summary>
	///   This constructor is only used by EF Core.
	/// </summary>
	private Candidate() : base(CandidateId.New())
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id where the candidate is applying.</param>
	/// <param name="name">The candidate's name.</param>
	/// <param name="email">The candidate's email.</param>
	private Candidate(
		JobOpportunityId jobOpportunityId,
		CandidateName name,
		CandidateEmail email) : base(CandidateId.New())
	{
		JobOpportunityId = jobOpportunityId;
		Name = name;
		Email = email;
	}

	/// <summary>
	///   Gets or sets the candidate's name.
	/// </summary>
	public CandidateName Name { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's email.
	/// </summary>
	public CandidateEmail Email { get; private set; }

	/// <summary>
	///   Creates a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id where the candidate is applying.</param>
	/// <param name="name">The candidate's name.</param>
	/// <param name="email">The candidate's email.</param>
	/// <returns>Returns a new instance of the <see cref="Candidate" /> class.</returns>
	public static Candidate Create(
		JobOpportunityId jobOpportunityId,
		CandidateName name,
		CandidateEmail email)
	{
		var candidate = new Candidate(jobOpportunityId, name, email);
		var @event = new CandidateCreatedEvent(email.Value);
		candidate.RaiseEvent(@event);

		return candidate;
	}

	#region Updates

	/// <summary>
	///   This method updates the candidate's name.
	/// </summary>
	/// <param name="name">The new candidate's name.</param>
	public void UpdateName(CandidateName? name) => Name = name ?? Name;

	#endregion

	#region EF Core Relationships

	/// <summary>
	///   Gets or sets the job opportunity id where the candidate is applying.
	/// </summary>
	public JobOpportunityId JobOpportunityId { get; private set; }

	/// <summary>
	///   Gets or sets the job opportunity where the candidate is applying.
	/// </summary>
	public JobOpportunity? JobOpportunity { get; private set; } = null!;

	#endregion
}