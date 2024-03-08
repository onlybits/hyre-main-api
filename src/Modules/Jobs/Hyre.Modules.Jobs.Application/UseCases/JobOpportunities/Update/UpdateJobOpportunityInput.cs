// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;

/// <summary>
///   This is the input data to update a job opportunity.
/// </summary>
/// <param name="Name">The new name of the job opportunity.</param>
/// <param name="Description">The new description of the job opportunity.</param>
/// <param name="Location">The new location of the job opportunity.</param>
/// <param name="Contract">The new contract of the job opportunity.</param>
/// <param name="Requirements">The new requirements of the job opportunity.</param>
public sealed record UpdateJobOpportunityInput(
	JobOpportunityName? Name,
	JobOpportunityDescription? Description,
	JobOpportunityLocation? Location,
	JobOpportunityContract? Contract,
	JobOpportunityRequirements? Requirements);