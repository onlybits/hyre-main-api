// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Application.Common;

/// <summary>
///   Represents the response of a job opportunity.
/// </summary>
/// <param name="Id">The job opportunity identifier.</param>
/// <param name="Name">The job opportunity name.</param>
/// <param name="Description">The job opportunity description.</param>
/// <param name="Location">The job opportunity location.</param>
/// <param name="Contract">The job opportunity contract.</param>
/// <param name="Requirements">The job opportunity requirements.</param>
/// <param name="CreatedAt">The job opportunity creation date.</param>
public sealed record JobOpportunityResponse(
	JobOpportunityId Id,
	JobOpportunityName Name,
	JobOpportunityDescription Description,
	JobOpportunityLocation? Location,
	JobOpportunityContract Contract,
	JobOpportunityRequirements? Requirements,
	CreateDate CreatedAt);