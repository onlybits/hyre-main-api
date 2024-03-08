// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Delete;

/// <summary>
///   Represents the request to delete a candidate.
/// </summary>
/// <param name="JobOpportunityId">The job opportunity id.</param>
/// <param name="CandidateId">The candidate id.</param>
/// <param name="CandidateTrackChanges">Should EF Core keep track of changes in the candidate entity.</param>
public sealed record DeleteCandidateRequest(
	JobOpportunityId JobOpportunityId,
	CandidateId CandidateId,
	bool CandidateTrackChanges) : IRequest;