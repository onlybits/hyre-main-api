// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;

/// <summary>
///   This use case is responsible for creating a candidate.
/// </summary>
/// <inheritdoc cref="ICreateCandidateUseCase" />
internal class CreateCandidateUseCase : ICreateCandidateUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public CreateCandidateUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task<CandidateResponse> Handle(CreateCandidateRequest request, CancellationToken cancellationToken)
	{
		var jobOpportunity = await _repository.JobOpportunity.FindByIdAsync(
			request.JobOpportunityId,
			request.JobOpportunityTrackChanges,
			true,
			cancellationToken);

		if (jobOpportunity is null)
		{
			_logger.LogError("Job opportunity with id {JobOpportunityId} not found.", request.JobOpportunityId);
			throw new JobOpportunityNotFoundException();
		}

		var candidate = Candidate.Create(request.JobOpportunityId, request.Input.Name);

		_repository.Candidate.Create(candidate);
		jobOpportunity.AddCandidate(candidate);
		await _repository.CommitChangesAsync(cancellationToken);

		_logger.LogInfo("Candidate {CandidateId} created for job opportunity {JobOpportunityId}.", candidate.Id, jobOpportunity.Id);

		return candidate.ToResponse();
	}
}