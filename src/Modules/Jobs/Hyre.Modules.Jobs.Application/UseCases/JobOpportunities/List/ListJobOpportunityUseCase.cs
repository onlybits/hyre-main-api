// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;

/// <summary>
///   This is the use case to list job opportunities.
/// </summary>
internal sealed class ListJobOpportunityUseCase : IListJobOpportunityUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public ListJobOpportunityUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task<ListJobOpportunityResponse> Handle(ListJobOpportunityRequest request,
		CancellationToken cancellationToken)
	{
		var pagedList = await _repository.JobOpportunity.ListAsync(request.Parameters, cancellationToken);
		var jobOpportunities = pagedList
			.Select(jobOpportunity => jobOpportunity.ToResponse())
			.ToList();

		_logger.LogInfo("{Count} Job opportunities listed successfully.", jobOpportunities.Count);
		return new ListJobOpportunityResponse(pagedList.MetaData, jobOpportunities);
	}
}