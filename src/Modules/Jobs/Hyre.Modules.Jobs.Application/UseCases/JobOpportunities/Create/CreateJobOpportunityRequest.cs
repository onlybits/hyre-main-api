// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Common;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;

/// <summary>
///   Represents the use case to create a new job opportunity.
/// </summary>
/// <param name="Input"></param>
public sealed record CreateJobOpportunityRequest(CreateJobOpportunityInput Input) : IRequest<JobOpportunityResponse>;