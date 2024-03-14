// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's date of birth.
/// </summary>
public sealed record CandidateDateOfBirth : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateDateOfBirth" /> class.
	/// </summary>
	/// <param name="value"></param>
	public CandidateDateOfBirth(DateOnly value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets the date of birth value.
	/// </summary>
	public DateOnly Value { get; }

	/// <summary>
	///   This method validates the <see cref="CandidateDateOfBirth" />.
	/// </summary>
	/// <exception cref="CandidateDateOfBirthNotValidException">Exception thrown when the date of birth is not valid.</exception>
	private void Validate()
	{
		if (Value > DateOnly.FromDateTime(DateTime.Now.AddYears(-16)))
		{
			throw new CandidateDateOfBirthNotValidException();
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts the <see cref="CandidateDateOfBirth" /> to a <see cref="DateOnly" />.
	/// </summary>
	/// <param name="dateOfBirth">The date of birth.</param>
	/// <returns>Returns the date of birth value.</returns>
	public static implicit operator DateOnly(CandidateDateOfBirth dateOfBirth) => dateOfBirth.Value;

	/// <summary>
	///   Implicitly converts the <see cref="DateOnly" /> to a <see cref="CandidateDateOfBirth" />.
	/// </summary>
	/// <param name="dateOfBirth">The date of birth.</param>
	/// <returns>Returns the date of birth value.</returns>
	public static implicit operator CandidateDateOfBirth(DateOnly dateOfBirth) => new(dateOfBirth);

	#endregion
}