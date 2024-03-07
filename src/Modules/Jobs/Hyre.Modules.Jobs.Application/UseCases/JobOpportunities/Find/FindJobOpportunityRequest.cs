// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Find;

/// <summary>
///   This is the request data to find a job opportunity by its id.
/// </summary>
/// <param name="Id">The job opportunity id.</param>
/// <param name="TrackChanges">Should EF keep track of the changes.</param>
public sealed record FindJobOpportunityRequest(JobOpportunityId Id, bool TrackChanges)
	: IRequest<JobOpportunityResponse>;