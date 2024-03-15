// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the speaking language.
/// </summary>
public sealed record CandidateLanguage : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateLanguage" /> class.
	/// </summary>
	/// <param name="language">The candidate's language.</param>
	/// <param name="proficiency">The candidate's proficiency.</param>
	public CandidateLanguage(string language, LanguageProficiency proficiency)
	{
		Language = language;
		Proficiency = proficiency;
	}

	/// <summary>
	///   Gets the candidate's language.
	/// </summary>
	public string Language { get; }

	/// <summary>
	///   Gets the candidate's proficiency.
	/// </summary>
	public LanguageProficiency Proficiency { get; }
}