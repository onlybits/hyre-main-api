// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Common;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;

/// <summary>
///   This is the use case contract to create a new job opportunity.
/// </summary>
internal interface ICreateJobOpportunityUseCase : IRequestHandler<CreateJobOpportunityRequest, JobOpportunityResponse>;