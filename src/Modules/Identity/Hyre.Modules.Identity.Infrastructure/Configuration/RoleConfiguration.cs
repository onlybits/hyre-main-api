// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Hyre.Modules.Identity.Infrastructure.Configuration;

/// <summary>
///   This class is responsible for configuring the <see cref="IdentityRole" /> entity.
/// </summary>
internal sealed class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		_ = builder.ToTable("roles");

		_ = builder.HasData(
			new IdentityRole
			{
				Name = UserRoles.Administrator.Name,
				NormalizedName = UserRoles.Administrator.Normalized
			});

		_ = builder.HasData(
			new IdentityRole
			{
				Name = UserRoles.Employee.Name,
				NormalizedName = UserRoles.Employee.Normalized
			});

		_ = builder.HasData(
			new IdentityRole
			{
				Name = UserRoles.Candidate.Name,
				NormalizedName = UserRoles.Candidate.Normalized
			});
	}
}