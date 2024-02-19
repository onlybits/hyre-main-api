// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Exceptions;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;

/// <summary>
///   Represents the use case to update a job opportunity.
/// </summary>
internal sealed class UpdateJobOpportunityUseCase : IUpdateJobOpportunityUseCase
{
	private readonly ILoggerManager _logger;
	private readonly IJobsRepositoryManager _repository;

	public UpdateJobOpportunityUseCase(IJobsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task Handle(UpdateJobOpportunityRequest request, CancellationToken cancellationToken)
	{
		var jobOpportunity = await _repository.JobOpportunity.FindByIdAsync(request.Id, request.TrackChanges, cancellationToken);
		if (jobOpportunity is null)
		{
			_logger.LogError("Job opportunity with id {Id} was not found.", request.Id);
			throw new JobOpportunityNotFoundException();
		}

		jobOpportunity.UpdateName(request.Input.Name);
		jobOpportunity.UpdateDescription(request.Input.Description);

		if (request.TrackChanges)
		{
			await _repository.CommitChangesAsync(cancellationToken);
		}
		else
		{
			_repository.JobOpportunity.Update(jobOpportunity);
			await _repository.CommitChangesAsync(cancellationToken);
		}

		_logger.LogInfo("Job opportunity with id {Id} updated successfully.", request.Id);
	}
}