// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the unique identifier of a candidate.
/// </summary>
public sealed record CandidateId : StronglyTypedId<Guid>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateId" /> class.
	/// </summary>
	/// <param name="Value">The value of the identifier.</param>
	public CandidateId(Guid Value) : base(Value) => Validate();

	/// <summary>
	///   Creates a new instance of the <see cref="CandidateId" /> class.
	/// </summary>
	/// <returns>Returns a new instance of the <see cref="CandidateId" /> class.</returns>
	public static CandidateId New() => new(Guid.NewGuid());

	/// <summary>
	///   This method is used to validate the identifier of the candidate.
	/// </summary>
	/// <exception cref="CandidateIdCannotBeEmptyException">Exception thrown when the identifier of the candidate is empty.</exception>
	private void Validate()
	{
		if (Value == Guid.Empty)
		{
			throw new CandidateIdCannotBeEmptyException();
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts the <see cref="CandidateId" /> to a <see cref="Guid" />.
	/// </summary>
	/// <param name="id">The identifier of the candidate.</param>
	/// <returns>Returns the value of the candidate identifier.</returns>
	public static implicit operator Guid(CandidateId id) => id.Value;

	/// <summary>
	///   Implicitly converts the <see cref="Guid" /> to a <see cref="CandidateId" />.
	/// </summary>
	/// <param name="id">The value of the identifier.</param>
	/// <returns>Returns a new instance of the <see cref="CandidateId" /> class.</returns>
	public static implicit operator CandidateId(Guid id) => new(id);

	#endregion
}