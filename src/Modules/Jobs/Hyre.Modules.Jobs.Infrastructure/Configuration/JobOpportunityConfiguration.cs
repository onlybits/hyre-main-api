// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Configuration;

/// <summary>
///   This class configures the mapping of the <see cref="JobOpportunity" /> entity to the database.
/// </summary>
internal sealed class JobOpportunityConfiguration : IEntityTypeConfiguration<JobOpportunity>
{
	public void Configure(EntityTypeBuilder<JobOpportunity> builder)
	{
		_ = builder.ToTable("JobOpportunities");

		_ = builder.HasKey(jo => jo.Id);
		_ = builder.Property(jo => jo.Id)
			.HasConversion(id => id.Value, value => new JobOpportunityId(value))
			.HasColumnName("id")
			.IsRequired();

		builder.Property(jo => jo.Name)
			.HasConversion(name => name.Value, value => new JobOpportunityName(value))
			.HasColumnName("name")
			.HasMaxLength(32)
			.IsRequired();

		builder.Ignore(jo => jo.Events);
	}
}