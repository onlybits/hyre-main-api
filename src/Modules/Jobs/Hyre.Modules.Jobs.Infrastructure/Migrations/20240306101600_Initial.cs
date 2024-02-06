using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
	_ =	migrationBuilder.EnsureSchema(
			name: "jobs");

		_ = migrationBuilder.CreateTable(
			name: "job_opportunities",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>(type: "uuid", nullable: false),
				name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
				description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
				created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
			},
			constraints: table => table.PrimaryKey("pk_job_opportunities", x => x.id));
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(
			name: "job_opportunities",
			schema: "jobs");
}