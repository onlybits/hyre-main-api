// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Delete;

/// <summary>
///   This is the use case to delete a candidate.
/// </summary>
internal sealed class DeleteCandidateUseCase : IDeleteCandidateUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public DeleteCandidateUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task Handle(DeleteCandidateRequest request, CancellationToken cancellationToken)
	{
		var jobOpportunityExists = await _repository.JobOpportunity.ExistsAsync(request.JobOpportunityId, cancellationToken);
		if (!jobOpportunityExists)
		{
			_logger.LogError("Job opportunity with id {JobOpportunityId} not found.", request.JobOpportunityId);
			throw new JobOpportunityNotFoundException();
		}

		var candidate = await _repository.Candidate.FindByIdAsync(request.JobOpportunityId, request.CandidateId, request.CandidateTrackChanges,
			cancellationToken);
		if (candidate is null)
		{
			_logger.LogError("Candidate with id {CandidateId} not found.", request.CandidateId);
			throw new CandidateNotFoundException();
		}

		_repository.Candidate.Delete(candidate);
		await _repository.CommitChangesAsync(cancellationToken);
		_logger.LogInfo("Candidate with id {CandidateId} deleted.", request.CandidateId);
	}
}