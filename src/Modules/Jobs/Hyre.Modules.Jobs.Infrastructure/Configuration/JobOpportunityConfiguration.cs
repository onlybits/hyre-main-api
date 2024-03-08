// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;
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
		_ = builder.ToTable("job_opportunities");

		_ = builder.HasKey(jo => jo.Id);
		_ = builder.Property(jo => jo.Id)
			.HasConversion(id => id.Value, value => new JobOpportunityId(value))
			.HasColumnName("id")
			.IsRequired();

		_ = builder.Property(jo => jo.Name)
			.HasConversion(name => name.Value, value => new JobOpportunityName(value))
			.HasColumnName("name")
			.HasMaxLength(32)
			.IsRequired();

		_ = builder.Property(jo => jo.Description)
			.HasConversion(description => description.Value, value => new JobOpportunityDescription(value))
			.HasColumnName("description")
			.HasMaxLength(500)
			.IsRequired();

		_ = builder.Property(jo => jo.CreatedAt)
			.HasConversion(jo => jo.Value, value => new CreateDate(value))
			.HasColumnName("created_at")
			.IsRequired();

		var location = builder.OwnsOne(jo => jo.Location);
		_ = location.Property(l => l.Type)
			.HasConversion<string>()
			.HasColumnName("location_type")
			.IsRequired();

		_ = location.Property(l => l.City)
			.HasColumnName("location_city")
			.HasMaxLength(32);

		_ = location.Property(l => l.State)
			.HasColumnName("location_state")
			.HasMaxLength(2);

		var contract = builder.OwnsOne(jo => jo.Contract);
		_ = contract.Property(c => c.Type)
			.HasConversion<string>()
			.HasColumnName("contract_type")
			.IsRequired();

		_ = contract.Property(c => c.MinSalary)
			.HasColumnName("contract_min_salary")
			.IsRequired();

		_ = contract.Property(c => c.MaxSalary)
			.HasColumnName("contract_max_salary")
			.IsRequired();

		_ = builder.OwnsOne(jo => jo.Requirements, v =>
		{
			_ = v.ToJson();
			_ = v.Property(x => x.Values).HasJsonPropertyName("values");
		});

		_ = builder.Ignore(jo => jo.Events);
	}
}