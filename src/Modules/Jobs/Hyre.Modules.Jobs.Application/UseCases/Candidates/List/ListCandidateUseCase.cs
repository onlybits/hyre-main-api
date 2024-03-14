// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   This is the use case to list candidates.
/// </summary>
internal sealed class ListCandidateUseCase : IListCandidateUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public ListCandidateUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task<ListCandidateResponse> Handle(ListCandidateRequest request, CancellationToken cancellationToken)
	{
		var jobOpportunityExists = await _repository.JobOpportunity.ExistsAsync(request.JobOpportunityId, cancellationToken);
		if (!jobOpportunityExists)
		{
			_logger.LogError("Job opportunity with id {JobOpportunityId} was not found!", request.JobOpportunityId);
			throw new JobOpportunityNotFoundException();
		}

		var candidates = await _repository.Candidate.ListAsync(
			request.Parameters,
			cancellationToken);

		_logger.LogInfo("{Count} Candidates listed successfully.", candidates.Count);
		return new ListCandidateResponse(candidates.MetaData, candidates);
	}
}