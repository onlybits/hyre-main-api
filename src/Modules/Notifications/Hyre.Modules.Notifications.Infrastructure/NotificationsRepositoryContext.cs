// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Notifications.Infrastructure;

/// <summary>
///   Represents the database context for the notifications repository.
/// </summary>
internal sealed class NotificationsRepositoryContext : DbContext
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationsRepositoryContext" /> class.
	/// </summary>
	/// <param name="options">The database context options.</param>
	public NotificationsRepositoryContext(DbContextOptions<NotificationsRepositoryContext> options) : base(options)
	{
	}

	/// <summary>
	///   Gets or sets the notifications.
	/// </summary>
	public DbSet<NotificationBase> Notifications => Set<NotificationBase>();

	/// <summary>
	///   Override this method to further configure the model that was discovered by convention from the entity types.
	/// </summary>
	/// <param name="modelBuilder">
	///   The builder being used to construct the model for this context.
	/// </param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		_ = modelBuilder.HasDefaultSchema("notifications");
		_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsRepositoryContext).Assembly);
	}
}