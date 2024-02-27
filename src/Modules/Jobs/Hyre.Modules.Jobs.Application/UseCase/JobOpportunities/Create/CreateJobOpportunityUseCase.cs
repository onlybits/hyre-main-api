// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Application.UseCase.JobOpportunities.Common;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCase.JobOpportunities.Create;

/// <summary>
///   Represents the use case to create a new job opportunity.
/// </summary>
/// <inheritdoc cref="ICreateJobOpportunityUseCase" />
internal sealed class CreateJobOpportunityUseCase : ICreateJobOpportunityUseCase
{
	private readonly IJobsRepositoryManager _repository;

	public CreateJobOpportunityUseCase(IJobsRepositoryManager repository) => _repository = repository;

	public async Task<JobOpportunityResponse> Handle(CreateJobOpportunityRequest request,
		CancellationToken cancellationToken)
	{
		var jobOpportunity = JobOpportunity.Create(request.Input.Name);
		_repository.JobOpportunity.Create(jobOpportunity);
		await _repository.SaveAsync(cancellationToken);

		return jobOpportunity.ToResponse();
	}
}