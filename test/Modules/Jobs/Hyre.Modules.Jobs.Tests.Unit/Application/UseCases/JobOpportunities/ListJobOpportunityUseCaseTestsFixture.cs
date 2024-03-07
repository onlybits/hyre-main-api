// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

public abstract class ListJobOpportunityUseCaseTestsFixture : JobOpportunityBaseFixture
{
	/// <summary>
	///   Generates a valid paged list of job opportunities.
	/// </summary>
	/// <param name="pageSize">The page size.</param>
	/// <param name="pageNumber">The current page number.</param>
	/// <param name="totalCount">The total number of items.</param>
	/// <returns>It will return a paged list of job opportunities.</returns>
	protected PagedList<JobOpportunity> GenerateValidPagedList(int pageSize, int pageNumber, int totalCount) => new(
		GenerateValidJobOpportunities(totalCount),
		totalCount,
		pageNumber,
		pageSize);

	/// <summary>
	///   Generates a valid list of job opportunities.
	/// </summary>
	/// <param name="max">The maximum number of job opportunities to generate.</param>
	/// <returns>It will return a list of job opportunities.</returns>
	private IEnumerable<JobOpportunity> GenerateValidJobOpportunities(int max) => Enumerable
		.Range(1, max)
		.Select(x => GenerateValidJobOpportunity())
		.ToList()
		.AsEnumerable();
}