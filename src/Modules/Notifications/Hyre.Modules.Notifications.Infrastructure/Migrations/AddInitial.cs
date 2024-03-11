// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Hyre.Modules.Notifications.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddInitial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			"notifications");

		migrationBuilder.CreateTable(
			"notifications",
			schema: "notifications",
			columns: table => new
			{
				id = table.Column<Guid>("uuid", nullable: false),
				recipient_type = table.Column<string>("text", nullable: false),
				recipient_address = table.Column<string>("text", nullable: false),
				created_at = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false)
			},
			constraints: table => { table.PrimaryKey("pk_notifications", x => x.id); });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(
			"notifications",
			"notifications");
}