// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Infrastructure.Persistence;
using Hyre.Shared.Abstractions.Kernel.Entities;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure;

/// <summary>
///   This class is responsible for managing the all the repositories for the jobs module.
/// </summary>
/// <inheritdoc cref="IJobsRepositoryManager" />
internal sealed class JobsRepositoryManager : IJobsRepositoryManager
{
	private readonly Lazy<ICandidateRepository> _candidateRepository;
	private readonly JobsRepositoryContext _context;
	private readonly Lazy<IJobOpportunityRepository> _jobOpportunityRepository;
	private readonly ILoggerManager _logger;
	private readonly IPublishEndpoint _publishEndpoint;

	/// <summary>
	///   Initializes a new instance of the <see cref="JobsRepositoryManager" /> class.
	/// </summary>
	/// <param name="context">The jobs repository context.</param>
	/// <param name="publishEndpoint">The publish endpoint for the message broker.</param>
	/// <param name="logger">The logger manager.</param>
	public JobsRepositoryManager(JobsRepositoryContext context, IPublishEndpoint publishEndpoint, ILoggerManager logger)
	{
		_context = context;
		_publishEndpoint = publishEndpoint;
		_logger = logger;
		_jobOpportunityRepository = new Lazy<IJobOpportunityRepository>(() => new JobOpportunityRepository(context));
		_candidateRepository = new Lazy<ICandidateRepository>(() => new CandidateRepository(context));
	}

	public ICandidateRepository Candidate => _candidateRepository.Value;

	public IJobOpportunityRepository JobOpportunity => _jobOpportunityRepository.Value;

	public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
	{
		await PublishEvents(cancellationToken);
		_ = await _context.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	///   This method publishes the domain events to the message broker.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	private async Task PublishEvents(CancellationToken cancellationToken)
	{
		var domainEvents = _context.ChangeTracker
			.Entries<IEntityBase>()
			.Select(entry => entry.Entity)
			.SelectMany(entity => entity.Events).ToList();

		_logger.LogInfo("Publishing {Count} domain events", domainEvents.Count);

		foreach (var domainEvent in domainEvents)
		{
			//NOTE: We are using dynamic here to send the concrete type of the domain event to the message broker.
			await _publishEndpoint.Publish((dynamic)domainEvent, cancellationToken);
			_logger.LogInfo("Published domain event {domainEvent}", domainEvent);
		}
	}
}