// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the name of a candidate.
/// </summary>
public sealed record CandidateName : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateName" /> class.
	/// </summary>
	/// <param name="firstName">The first name of the candidate.</param>
	/// <param name="middleName">The middle name of the candidate.</param>
	/// <param name="lastName">The last name of the candidate.</param>
	public CandidateName(string firstName, string? middleName, string lastName)
	{
		FirstName = firstName;
		MiddleName = middleName;
		LastName = lastName;
		Validate();
	}

	/// <summary>
	///   Gets the first name of the candidate.
	/// </summary>
	public string FirstName { get; }

	/// <summary>
	///   Gets the middle name of the candidate.
	/// </summary>
	public string? MiddleName { get; }

	/// <summary>
	///   Gets the last name of the candidate.
	/// </summary>
	public string LastName { get; }

	/// <summary>
	///   The method that validates the <see cref="CandidateName" />.
	/// </summary>
	/// <exception cref="CandidateFirstNameTooShortException">Exception thrown when the first name is too short.</exception>
	/// <exception cref="CandidateFirstNameTooLongException">Exception thrown when the first name is too long.</exception>
	/// <exception cref="CandidateMiddleNameTooShortException">Exception thrown when the middle name is too short.</exception>
	/// <exception cref="CandidateMiddleNameTooLongException">Exception thrown when the middle name is too long.</exception>
	/// <exception cref="CandidateLastNameTooShortException">Exception thrown when the last name is too short.</exception>
	/// <exception cref="CandidateLastNameTooLongException">Exception thrown when the last name is too long.</exception>
	private void Validate()
	{
		switch (FirstName.Length)
		{
			case < 3:
				throw new CandidateFirstNameTooShortException();
			case > 32:
				throw new CandidateFirstNameTooLongException();
		}

		if (MiddleName is not null)
		{
			switch (MiddleName.Length)
			{
				case < 3:
					throw new CandidateMiddleNameTooShortException();
				case > 32:
					throw new CandidateMiddleNameTooLongException();
			}
		}

		switch (LastName.Length)
		{
			case < 3:
				throw new CandidateLastNameTooShortException();
			case > 32:
				throw new CandidateLastNameTooLongException();
		}
	}
}