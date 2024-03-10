// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.ComponentModel.DataAnnotations;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the email of a candidate.
/// </summary>
public sealed record CandidateEmail : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateEmail" /> class.
	/// </summary>
	/// <param name="value"></param>
	public CandidateEmail(string value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets the email of the candidate.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   The method that validates the <see cref="CandidateEmail" />.
	/// </summary>
	/// <exception cref="CandidateEmailInvalidException">Exception thrown when the email is invalid.</exception>
	private void Validate()
	{
		if (!new EmailAddressAttribute().IsValid(Value))
		{
			throw new CandidateEmailInvalidException(Value);
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts a <see cref="CandidateEmail" /> to a <see cref="string" />.
	/// </summary>
	/// <param name="candidateEmail">The <see cref="CandidateEmail" /> to convert.</param>
	/// <returns>Returns the <see cref="string" /> representation of the <see cref="CandidateEmail" />.</returns>
	public static implicit operator string(CandidateEmail candidateEmail) => candidateEmail.Value;

	/// <summary>
	///   Implicitly converts a <see cref="string" /> to a <see cref="CandidateEmail" />.
	/// </summary>
	/// <param name="value">The <see cref="string" /> to convert.</param>
	/// <returns>Returns the <see cref="CandidateEmail" /> representation of the <see cref="string" />.</returns>
	public static implicit operator CandidateEmail(string value) => new(value);

	#endregion
}