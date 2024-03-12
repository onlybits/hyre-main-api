// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Identity.Infrastructure;

/// <summary>
///   Represents the identity repository context.
/// </summary>
internal sealed class IdentityRepositoryContext : IdentityDbContext<User>
{
	/// <summary>
	///   Override this method to further configure the model that was discovered by convention from the entity types.
	/// </summary>
	/// <param name="modelBuilder">
	///   The builder being used to construct the model for this context.
	/// </param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		_ = modelBuilder.HasDefaultSchema("identity");
		_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityRepositoryContext).Assembly);
	}
}