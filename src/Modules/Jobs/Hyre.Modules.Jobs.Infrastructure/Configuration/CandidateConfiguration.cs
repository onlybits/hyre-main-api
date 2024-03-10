// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Configuration;

/// <summary>
///   This class configures the mapping of the <see cref="Candidate" /> entity to the database.
/// </summary>
internal sealed class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
	public void Configure(EntityTypeBuilder<Candidate> builder)
	{
		_ = builder.ToTable("candidates");

		_ = builder.HasKey(c => c.Id);
		_ = builder.Property(c => c.Id)
			.HasConversion(id => id.Value, value => new CandidateId(value))
			.HasColumnName("id")
			.IsRequired();

		var name = builder.OwnsOne(c => c.Name);
		_ = name.Property(n => n.FirstName)
			.HasColumnName("first_name")
			.HasMaxLength(32)
			.IsRequired();

		_ = name.Property(n => n.MiddleName)
			.HasColumnName("middle_name")
			.HasMaxLength(32)
			.IsRequired();

		_ = name.Property(n => n.LastName)
			.HasColumnName("last_name")
			.HasMaxLength(32)
			.IsRequired();

		_ = builder.Property(c => c.Email)
			.HasConversion(e => e.Value, value => new CandidateEmail(value))
			.HasColumnName("email")
			.IsRequired();
		
		_ = builder.Property(c => c.CreatedAt)
			.HasConversion(c => c.Value, value => new CreateDate(value))
			.HasColumnName("created_at")
			.IsRequired();

		_ = builder.HasOne(c => c.JobOpportunity)
			.WithMany(jo => jo.Candidates)
			.HasForeignKey(c => c.JobOpportunityId)
			.OnDelete(DeleteBehavior.Cascade);

		_ = builder.Ignore(c => c.Events);
	}
}