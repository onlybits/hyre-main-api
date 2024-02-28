// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;

/// <summary>
///   This is the use case contract to list job opportunities.
/// </summary>
internal interface IListJobOpportunityUseCase : IRequestHandler<ListJobOpportunityRequest, ListJobOpportunityResponse>;