// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;

/// <summary>
///   Represents the request for the create candidate use case.
/// </summary>
/// <param name="JobOpportunityId">The job opportunity id where the candidate is applying.</param>
/// <param name="Input">The input data for the candidate.</param>
/// <param name="JobOpportunityTrackChanges">If the job opportunity should track changes.</param>
public sealed record CreateCandidateRequest(
	JobOpportunityId JobOpportunityId,
	CreateCandidateInput Input,
	bool JobOpportunityTrackChanges) : IRequest<CandidateResponse>;