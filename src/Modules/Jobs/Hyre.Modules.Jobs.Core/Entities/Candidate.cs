// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Jobs.Core.Entities;

/// <summary>
///   Represents a candidate for a job opportunity in the system.
/// </summary>
public sealed class Candidate : EntityBase<CandidateId>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="name">The candidate's name.</param>
	private Candidate(CandidateName name) : base(CandidateId.New()) => Name = name;

	/// <summary>
	///   Gets or sets the candidate's name.
	/// </summary>
	public CandidateName Name { get; private set; }

	/// <summary>
	///   Creates a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="name">The candidate's name.</param>
	/// <returns>Returns a new instance of the <see cref="Candidate" /> class.</returns>
	public static Candidate Create(CandidateName name)
	{
		var candidate = new Candidate(name);

		var @event = new CandidateCreatedEvent();
		candidate.RaiseEvent(@event);

		return candidate;
	}
}