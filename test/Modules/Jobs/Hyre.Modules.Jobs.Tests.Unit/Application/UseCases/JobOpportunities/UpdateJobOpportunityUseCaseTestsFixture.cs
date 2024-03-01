// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Data provider for the <see cref="UpdateJobOpportunityUseCaseTests" /> class.
/// </summary>
public abstract class UpdateJobOpportunityUseCaseTestsFixture : BaseFixture
{
	/// <summary>
	///   Generates a valid <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateValidJobOpportunity() => JobOpportunity.Create(GenerateValidName());

	/// <summary>
	///   Generates a valid request for the <see cref="UpdateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <param name="trackChanges">Should EF keep track of the changes?</param>
	/// <returns>It will return a valid <see cref="UpdateJobOpportunityRequest" />.</returns>
	protected UpdateJobOpportunityRequest GenerateValidRequest(bool trackChanges) => new(
		JobOpportunityId.New(),
		GenerateValidInput(),
		trackChanges);

	/// <summary>
	///   Generates a valid input for the <see cref="UpdateJobOpportunityRequest" /> record.
	/// </summary>
	/// <returns>It will return a valid <see cref="UpdateJobOpportunityInput" />.</returns>
	private UpdateJobOpportunityInput GenerateValidInput() => new(GenerateValidName());

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityName" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityName" />.</returns>
	private JobOpportunityName GenerateValidName() => new(Faker.Name.JobTitle().ClampLength(3, 32));
}