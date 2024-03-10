// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Modules.Notifications.Core.ValueObjects;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Hyre.Modules.Notifications.Infrastructure.Configuration;

/// <summary>
///   This class configures the mapping of the <see cref="Notification" /> entity to the database.
/// </summary>
internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		_ = builder.ToTable("notifications");

		_ = builder.HasKey(n => n.Id);
		_ = builder.Property(n => n.Id)
			.HasConversion(id => id.Value, value => new NotificationId(value))
			.HasColumnName("id")
			.IsRequired();

		var recipient = builder.OwnsOne(n => n.Recipient);
		_ = recipient.Property(r => r.Type)
			.HasConversion<string>()
			.HasColumnName("recipient_type")
			.IsRequired();

		_ = recipient.Property(r => r.Address)
			.HasColumnName("recipient_address")
			.IsRequired();

		_ = builder.Property(n => n.CreatedAt)
			.HasConversion(n => n.Value, value => new CreateDate(value))
			.HasColumnName("created_at")
			.IsRequired();

		_ = builder.Ignore(n => n.Events);
	}
}