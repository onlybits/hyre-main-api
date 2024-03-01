// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;

/// <summary>
///   This is the request for deleting a job opportunity.
/// </summary>
/// <param name="Id">The job opportunity id to be deleted.</param>
/// <param name="TrackChanges">Should EF keep track of the changes?</param>
public sealed record DeleteJobOpportunityRequest(JobOpportunityId Id, bool TrackChanges) : IRequest;