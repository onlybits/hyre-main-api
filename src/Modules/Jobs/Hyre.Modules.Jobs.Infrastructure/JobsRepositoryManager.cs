// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Infrastructure.Persistence;

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

	/// <summary>
	///   Initializes a new instance of the <see cref="JobsRepositoryManager" /> class.
	/// </summary>
	/// <param name="context">The jobs repository context.</param>
	public JobsRepositoryManager(JobsRepositoryContext context)
	{
		_context = context;
		_jobOpportunityRepository = new Lazy<IJobOpportunityRepository>(() => new JobOpportunityRepository(context));
		_candidateRepository = new Lazy<ICandidateRepository>(() => new CandidateRepository(context));
	}

	public ICandidateRepository Candidate => _candidateRepository.Value;

	public IJobOpportunityRepository JobOpportunity => _jobOpportunityRepository.Value;

	public async Task CommitChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
}