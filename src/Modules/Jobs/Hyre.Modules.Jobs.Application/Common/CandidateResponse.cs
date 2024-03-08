// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Application.Common;

/// <summary>
///   Represents the response of a candidate.
/// </summary>
/// <param name="Id">The candidate identifier.</param>
/// <param name="Name">The candidate's name.</param>
/// <param name="CreatedAt">The candidate creation date.</param>
/// <param name="JobOpportunityId">The job opportunity identifier.</param>
public sealed record CandidateResponse(
	CandidateId Id,
	CandidateName Name,
	CreateDate CreatedAt,
	JobOpportunityId JobOpportunityId);