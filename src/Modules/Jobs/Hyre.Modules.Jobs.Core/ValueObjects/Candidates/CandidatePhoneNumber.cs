// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's phone number.
/// </summary>
public sealed record CandidatePhoneNumber : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidatePhoneNumber" /> class.
	/// </summary>
	/// <param name="areaCode">The phone number area code.</param>
	/// <param name="number">The phone number value.</param>
	public CandidatePhoneNumber(string areaCode, string number)
	{
		AreaCode = areaCode;
		Number = number;
		Validate();
	}

	/// <summary>
	///   Gets the phone number value.
	/// </summary>
	public string AreaCode { get; }

	/// <summary>
	///   Gets the phone number value.
	/// </summary>
	public string Number { get; }

	/// <summary>
	///   This method validates the <see cref="CandidatePhoneNumber" />.
	/// </summary>
	/// <exception cref="CandidatePhoneNumberAreaCodeNotValidException">
	///   Exception thrown when the phone number area code is not
	///   valid.
	/// </exception>
	/// <exception cref="CandidatePhoneNumberValueNotValidException">Exception thrown when the phone number value is not valid.</exception>
	private void Validate()
	{
		if (AreaCode.Length != 2 || IsNumeric(AreaCode))
		{
			throw new CandidatePhoneNumberAreaCodeNotValidException();
		}

		{
			if (Number.Length != 11 || IsNumeric(Number))
			{
				throw new CandidatePhoneNumberValueNotValidException();
			}
		}

		return;

		// Local function to check if the input is numeric.
		static bool IsNumeric(string input)
		{
			var isValid = int.TryParse(input, out _);
			return isValid;
		}
	}
}