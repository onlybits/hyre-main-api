// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's seniority.
/// </summary>
public sealed record CandidateSeniority : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateSeniority" /> class.
	/// </summary>
	/// <param name="value">The candidate's seniority value.</param>
	public CandidateSeniority(ExperienceLevel value) => Value = value;

	/// <summary>
	///   Gets the candidate's seniority value.
	/// </summary>
	public ExperienceLevel Value { get; }
}