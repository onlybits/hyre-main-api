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
		#region Id

		_ = builder.HasKey(c => c.Id);
		_ = builder.Property(c => c.Id)
			.HasConversion(id => id.Value, value => new CandidateId(value))
			.HasColumnName("id")
			.IsRequired();

		#endregion

		#region Name

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

		#endregion

		#region Email

		_ = builder.Property(c => c.Email)
			.HasConversion(e => e.Value, value => new CandidateEmail(value))
			.HasColumnName("email")
			.IsRequired();

		_ = builder.HasIndex(c => c.Email)
			.IsUnique();

		#endregion

		#region Document

		_ = builder.Property(c => c.Document)
			.HasConversion(d => d.Value, value => new CandidateDocument(value))
			.HasColumnName("document")
			.IsRequired();

		#endregion

		#region Date Of Birth

		_ = builder.Property(c => c.DateOfBirth)
			.HasConversion(d => d.Value, value => new CandidateDateOfBirth(value))
			.HasColumnName("date_of_birth")
			.IsRequired();

		#endregion

		#region Seniority

		_ = builder.Property(c => c.Seniority)
			.HasConversion(s => s.Value, value => new CandidateSeniority(value))
			.HasColumnName("seniority")
			.IsRequired();

		#endregion

		#region Disability

		var disability = builder.OwnsOne(c => c.Disability);

		_ = disability.Property(d => d.Hearing)
			.HasColumnName("disability_hearing");

		_ = disability.Property(d => d.Intellectual)
			.HasColumnName("disability_intellectual");

		_ = disability.Property(d => d.Physical)
			.HasColumnName("disability_physical");

		_ = disability.Property(d => d.Vision)
			.HasColumnName("disability_vision");

		#endregion

		#region Gender

		_ = builder.Property(c => c.Gender)
			.HasConversion(g => g.Value, value => new CandidateGender(value))
			.HasColumnName("gender")
			.IsRequired();

		#endregion

		#region PhoneNumber

		var phoneNumber = builder.OwnsOne(c => c.PhoneNumber);
		_ = phoneNumber.Property(p => p.AreaCode)
			.HasColumnName("phone_area_code")
			.HasMaxLength(2)
			.IsRequired();

		_ = phoneNumber.Property(p => p.Number)
			.HasColumnName("phone_number")
			.HasMaxLength(9)
			.IsRequired();

		#endregion

		#region Address

		var address = builder.OwnsOne(c => c.Address);

		_ = address.Property(a => a.Country)
			.HasColumnName("address_country")
			.HasMaxLength(50)
			.IsRequired();

		_ = address.Property(a => a.State)
			.HasColumnName("address_state")
			.HasMaxLength(50)
			.IsRequired();

		_ = address.Property(a => a.Neighborhood)
			.HasColumnName("address_neighborhood")
			.HasMaxLength(50)
			.IsRequired();

		_ = address.Property(a => a.Complement)
			.HasMaxLength(100)
			.HasColumnName("address_complement");

		_ = address.Property(a => a.City)
			.HasColumnName("address_city")
			.HasMaxLength(50)
			.IsRequired();

		_ = address.Property(a => a.Number)
			.HasColumnName("address_number")
			.IsRequired();

		_ = address.Property(a => a.ZipCode)
			.HasColumnName("address_zip_code")
			.HasMaxLength(8)
			.IsRequired();

		#endregion

		#region Social Network

		var socialNetwork = builder.OwnsOne(c => c.SocialNetwork);

		_ = socialNetwork.Property(sn => sn.GitHub)
			.HasColumnName("social_network_github")
			.HasMaxLength(32);

		_ = socialNetwork.Property(sn => sn.LinkedIn)
			.HasColumnName("social_network_linkedin")
			.HasMaxLength(32);

		#endregion

		#region Date of	Creation

		_ = builder.Property(c => c.CreatedAt)
			.HasConversion(c => c.Value, value => new CreateDate(value))
			.HasColumnName("created_at")
			.IsRequired();

		#endregion

		#region Education

		_ = builder.OwnsMany(c => c.Educations, ba =>
		{
			_ = ba.ToJson();
			_ = ba.Property(e => e.StartDate).HasJsonPropertyName("start_date");
			_ = ba.Property(e => e.EndDate).HasJsonPropertyName("end_date");
			_ = ba.Property(e => e.Institution).HasJsonPropertyName("institution");
			_ = ba.Property(e => e.Course).HasJsonPropertyName("course");
			_ = ba.Property(e => e.Degree).HasJsonPropertyName("degree");
		});

		#endregion

		#region Experience

		_ = builder.OwnsMany(c => c.Experiences, v =>
		{
			_ = v.ToJson();
			_ = v.Property(e => e.StartDate).HasJsonPropertyName("start_date");
			_ = v.Property(e => e.EndDate).HasJsonPropertyName("end_date");
			_ = v.Property(e => e.Company).HasJsonPropertyName("company");
			_ = v.Property(e => e.Position).HasJsonPropertyName("position");
			_ = v.Property(e => e.Description).HasJsonPropertyName("description");
		});

		#endregion

		#region Language

		_ = builder.OwnsMany(c => c.Languages, v =>
		{
			_ = v.ToJson();
			_ = v.Property(l => l.Language).HasJsonPropertyName("language");
			_ = v.Property(l => l.Proficiency).HasJsonPropertyName("proficiency");
		});

		#endregion

		#region Relationships

		_ = builder.HasMany(c => c.JobOpportunities)
			.WithMany(jo => jo.Candidates);

		#endregion

		#region Base

		_ = builder.ToTable("candidates");

		_ = builder.Ignore(c => c.Events);

		#endregion
	}
}