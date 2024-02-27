// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCase.JobOpportunities.Create;

/// <summary>
///   Represents the input data to create a new job opportunity.
/// </summary>
/// <param name="Name">The name of the job opportunity.</param>
public sealed record CreateJobOpportunityInput(
	JobOpportunityName Name);