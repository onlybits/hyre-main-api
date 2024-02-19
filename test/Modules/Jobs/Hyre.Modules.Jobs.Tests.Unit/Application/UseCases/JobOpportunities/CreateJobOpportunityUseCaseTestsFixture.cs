// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Data provider for the <see cref="CreateJobOpportunityUseCaseTests" /> class.
/// </summary>
public abstract class CreateJobOpportunityUseCaseTestsFixture : JobOpportunityBaseFixture
{
	/// <summary>
	///   Generates a valid request for the <see cref="CreateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="CreateJobOpportunityRequest" />.</returns>
	protected CreateJobOpportunityRequest GenerateValidRequest() => new(GenerateValidInput());

	/// <summary>
	///   Generates a valid input for the <see cref="CreateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="CreateJobOpportunityInput" />.</returns>
	private CreateJobOpportunityInput GenerateValidInput() => new(
		GenerateValidName(),
		GenerateValidDescription());
}