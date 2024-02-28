// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Requests;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;

/// <summary>
///   This is the request for the list job opportunities use case.
/// </summary>
/// <param name="Parameters">The parameters to filter the job opportunities.</param>
public sealed record ListJobOpportunityRequest(
	JobOpportunityParameters Parameters) : IRequest<ListJobOpportunityResponse>;