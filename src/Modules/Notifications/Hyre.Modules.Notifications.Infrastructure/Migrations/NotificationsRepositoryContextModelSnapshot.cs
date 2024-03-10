﻿// <auto-generated />
using System;
using Hyre.Modules.Notifications.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hyre.Modules.Notifications.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationsRepositoryContext))]
    partial class NotificationsRepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("notifications")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Hyre.Modules.Notifications.Core.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id")
                        .HasName("pk_notifications");

                    b.ToTable("notifications", "notifications");
                });

            modelBuilder.Entity("Hyre.Modules.Notifications.Core.Entities.Notification", b =>
                {
                    b.OwnsOne("Hyre.Modules.Notifications.Core.ValueObjects.NotificationRecipient", "Recipient", b1 =>
                        {
                            b1.Property<Guid>("NotificationId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("recipient_address");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("recipient_type");

                            b1.HasKey("NotificationId");

                            b1.ToTable("notifications", "notifications");

                            b1.WithOwner()
                                .HasForeignKey("NotificationId")
                                .HasConstraintName("fk_notifications_notifications_id");
                        });

                    b.Navigation("Recipient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
