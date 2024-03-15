// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the summary of the candidate.
/// </summary>
public sealed record CandidateSummary : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateSummary" /> class.
	/// </summary>
	/// <param name="value"></param>
	public CandidateSummary(string value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets the summary value.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   This method validates the <see cref="CandidateSummary" />.
	/// </summary>
	/// <exception cref="CandidateSummaryValueNotValidException"></exception>
	private void Validate()
	{
		if (Value.Length is > 1000 or < 10)
		{
			throw new CandidateSummaryValueNotValidException();
		}
	}
}