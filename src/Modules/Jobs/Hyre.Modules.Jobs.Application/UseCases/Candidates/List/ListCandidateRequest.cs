// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   Represents the request to list candidates.
/// </summary>
/// <param name="JobOpportunityId">The job opportunity id.</param>
/// <param name="JobOpportunityTrackChanges">Should EF Core track changes for the job opportunity.</param>
/// <param name="Parameters">The parameters to filter the candidates.</param>
public sealed record ListCandidateRequest(
	JobOpportunityId JobOpportunityId,
	bool JobOpportunityTrackChanges,
	CandidateParameters Parameters) : IRequest<ListCandidateResponse>;