// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's document.
/// </summary>
public sealed record CandidateDocument : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateDocument" /> class.
	/// </summary>
	/// <param name="value"></param>
	public CandidateDocument(string value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets the document value.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   This method validates the <see cref="CandidateDocument" />.
	/// </summary>
	/// <exception cref="Exception"></exception>
	private void Validate()
	{
		if (!IsValid(Value))
		{
			throw new CandidateDocumentNotValidException();
		}

		return;

		// Local function to validate the document.
		static bool IsValid(string value)
		{
			value = new string(value.Where(char.IsDigit).ToArray());

			if (value.Length != 11)
			{
				return false;
			}

			if (value.Distinct().Count() == 1)
			{
				return false;
			}

			var sum = 0;
			for (var i = 0; i < 9; i++)
			{
				sum += (value[i] - '0') * (10 - i);
			}

			var remainder = sum % 11;
			var firstDigit = remainder < 2 ? 0 : 11 - remainder;

			sum = 0;
			for (var i = 0; i < 10; i++)
			{
				sum += (value[i] - '0') * (11 - i);
			}

			remainder = sum % 11;
			var secondDigit = remainder < 2 ? 0 : 11 - remainder;

			return value[9] - '0' == firstDigit && value[10] - '0' == secondDigit;
		}
	}
}