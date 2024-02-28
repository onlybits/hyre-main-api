// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Common;

/// <summary>
///   Represents the response of a job opportunity.
/// </summary>
/// <param name="Id">The job opportunity identifier.</param>
/// <param name="Name">The job opportunity name.</param>
public sealed record JobOpportunityResponse(
	JobOpportunityId Id,
	JobOpportunityName Name);