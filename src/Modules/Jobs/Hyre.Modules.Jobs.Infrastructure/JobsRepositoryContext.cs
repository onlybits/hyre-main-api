// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure;

/// <summary>
///   Represents the database context for the jobs repository.
/// </summary>
internal sealed class JobsRepositoryContext : DbContext
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobsRepositoryContext" /> class.
	/// </summary>
	/// <param name="options">The database context options.</param>
	public JobsRepositoryContext(DbContextOptions<JobsRepositoryContext> options) : base(options)
	{
	}

	/// <summary>
	///   Gets or sets the candidates.
	/// </summary>
	public DbSet<Candidate> Candidates => Set<Candidate>();

	/// <summary>
	///   Gets or sets the job opportunities.
	/// </summary>
	public DbSet<JobOpportunity> JobOpportunities => Set<JobOpportunity>();

	/// <summary>
	///   Override this method to further configure the model that was discovered by convention from the entity types.
	/// </summary>
	/// <param name="modelBuilder">
	///   The builder being used to construct the model for this context.
	/// </param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		_ = modelBuilder.HasDefaultSchema("jobs");
		_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsRepositoryContext).Assembly);
	}
}