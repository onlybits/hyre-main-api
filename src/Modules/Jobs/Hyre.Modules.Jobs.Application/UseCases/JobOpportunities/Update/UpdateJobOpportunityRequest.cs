// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;

/// <summary>
///   This is the request to update a job opportunity.
/// </summary>
/// <param name="Id">The id of the job opportunity to be updated.</param>
/// <param name="Input">The input data to update the job opportunity.</param>
/// <param name="TrackChanges">Should EF keep track of the changes.</param>
public sealed record UpdateJobOpportunityRequest(
	JobOpportunityId Id,
	UpdateJobOpportunityInput Input,
	bool TrackChanges) : IRequest;