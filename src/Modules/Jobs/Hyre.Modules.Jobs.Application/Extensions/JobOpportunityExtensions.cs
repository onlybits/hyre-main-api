// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using Hyre.Modules.Jobs.Core.Entities;

#endregion

namespace Hyre.Modules.Jobs.Application.Extensions;

/// <summary>
///   The extensions for the job opportunity entity.
/// </summary>
public static class JobOpportunityExtensions
{
	/// <summary>
	///   This method converts a job opportunity to a job opportunity response.
	/// </summary>
	/// <param name="jobOpportunity">The job opportunity to be converted.</param>
	/// <returns>Returns a job opportunity response.</returns>
	public static JobOpportunityResponse ToResponse(this JobOpportunity jobOpportunity) => new(
		jobOpportunity.Id,
		jobOpportunity.Name,
		jobOpportunity.Description,
		jobOpportunity.Location,
		jobOpportunity.Contract,
		jobOpportunity.Requirements,
		jobOpportunity.CreatedAt);
}