// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's social network.
/// </summary>
public sealed record CandidateSocialNetwork : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateSocialNetwork" /> class.
	/// </summary>
	/// <param name="linkedIn">The candidate's LinkedIn.</param>
	/// <param name="gitHub">The candidate's GitHub.</param>
	public CandidateSocialNetwork(string? linkedIn, string? gitHub)
	{
		LinkedIn = linkedIn;
		GitHub = gitHub;
	}

	/// <summary>
	///   Gets the candidate's LinkedIn.
	/// </summary>
	public string? LinkedIn { get; }

	/// <summary>
	///   Gets the candidate's GitHub.
	/// </summary>
	public string? GitHub { get; }
}