// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Delete;

/// <summary>
///   Represents the request to delete a candidate.
/// </summary>
/// <param name="CandidateId">The candidate id.</param>
/// <param name="CandidateTrackChanges">Should EF Core keep track of changes in the candidate entity.</param>
public sealed record DeleteCandidateRequest(
	CandidateId CandidateId,
	bool CandidateTrackChanges) : IRequest;