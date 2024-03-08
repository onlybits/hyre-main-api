// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;

/// <summary>
///   This is the use case to delete a job opportunity by its id.
/// </summary>
internal sealed class DeleteJobOpportunityUseCase : IDeleteJobOpportunityUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public DeleteJobOpportunityUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task Handle(DeleteJobOpportunityRequest request, CancellationToken cancellationToken)
	{
		var jobOpportunity = await _repository
			.JobOpportunity
			.FindByIdAsync(request.Id, request.TrackChanges, cancellationToken: cancellationToken);

		if (jobOpportunity is null)
		{
			_logger.LogError("Job opportunity with id {Id} was not found.", request.Id);
			throw new JobOpportunityNotFoundException();
		}

		_repository.JobOpportunity.Delete(jobOpportunity);
		await _repository.CommitChangesAsync(cancellationToken);
		_logger.LogInfo("Job opportunity with id {Id} was deleted.", request.Id);
	}
}