// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Common;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;

/// <summary>
///   Represents the use case to create a new job opportunity.
/// </summary>
/// <inheritdoc cref="ICreateJobOpportunityUseCase" />
internal sealed class CreateJobOpportunityUseCase : ICreateJobOpportunityUseCase
{
	private readonly ILoggerManager<CreateJobOpportunityUseCase> _logger;
	private readonly IJobsRepositoryManager _repository;

	public CreateJobOpportunityUseCase(
		IJobsRepositoryManager repository,
		ILoggerManager<CreateJobOpportunityUseCase> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task<JobOpportunityResponse> Handle(CreateJobOpportunityRequest request,
		CancellationToken cancellationToken)
	{
		var jobOpportunity = JobOpportunity.Create(request.Input.Name);
		try
		{
			_repository.JobOpportunity.Create(jobOpportunity);
			await _repository.SaveAsync(cancellationToken);

			_logger.LogInfo("Job opportunity: {JobOpportunity} created successfully.", jobOpportunity);
			return jobOpportunity.ToResponse();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "Error creating job opportunity: {JobOpportunity}.", jobOpportunity);
			throw;
		}
	}
}