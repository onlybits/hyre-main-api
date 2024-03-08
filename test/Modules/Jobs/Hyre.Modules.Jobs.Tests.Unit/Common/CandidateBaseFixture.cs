// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Common;

/// <summary>
///   The base fixture for the <see cref="Candidate" /> class.
/// </summary>
public abstract class CandidateBaseFixture : BaseFixture
{
	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>The valid <see cref="Candidate" />.</returns>
	protected Candidate GenerateValidCandidate() => Candidate.Create(
		GenerateValidName());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	protected CandidateName GenerateValidName() => new(
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.LastName().ClampLength(3, 32)
	);
}