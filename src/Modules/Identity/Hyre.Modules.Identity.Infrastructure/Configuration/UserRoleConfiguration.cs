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
///   This class is responsible for configuring the <see cref="IdentityUserClaim{TKey}" /> entity.
/// </summary>
internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) => _ = builder.ToTable("user_roles");
}