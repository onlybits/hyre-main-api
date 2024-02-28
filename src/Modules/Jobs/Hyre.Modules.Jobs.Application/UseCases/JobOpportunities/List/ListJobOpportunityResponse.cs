// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Common;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;

/// <summary>
///   This is the response for the list job opportunities use case.
/// </summary>
/// <param name="MetaData">The metadata containing the pagination information.</param>
/// <param name="JobOpportunities">The list of job opportunities.</param>
public sealed record ListJobOpportunityResponse(
	MetaData MetaData,
	IReadOnlyList<JobOpportunityResponse> JobOpportunities);