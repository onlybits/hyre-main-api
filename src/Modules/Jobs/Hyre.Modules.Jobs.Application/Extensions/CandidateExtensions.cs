// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using Hyre.Modules.Jobs.Core.Entities;

#endregion

namespace Hyre.Modules.Jobs.Application.Extensions;

/// <summary>
///   The extensions for the candidate entity.
/// </summary>
public static class CandidateExtensions
{
	/// <summary>
	///   This method converts a candidate to a candidate response.
	/// </summary>
	/// <param name="candidate">The candidate to be converted.</param>
	/// <returns>Returns a candidate response.</returns>
	public static CandidateResponse ToResponse(this Candidate candidate) => new(
		candidate.Id,
		candidate.Name,
		candidate.CreatedAt,
		candidate.JobOpportunityId);
}