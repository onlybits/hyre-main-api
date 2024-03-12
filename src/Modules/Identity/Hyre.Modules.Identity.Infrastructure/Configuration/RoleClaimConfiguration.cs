// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Hyre.Modules.Identity.Infrastructure.Configuration;

/// <summary>
///   This class is responsible for configuring the <see cref="IdentityUserToken{TKey}" /> entity.
/// </summary>
internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
	public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder) => _ = builder.ToTable("role_claims");
}