// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddInitialJobOpportunities : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			"jobs");

		migrationBuilder.CreateTable(
			"job_opportunities",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>("uuid", nullable: false),
				name = table.Column<string>("character varying(32)", maxLength: 32, nullable: false),
				description = table.Column<string>("character varying(500)", maxLength: 500, nullable: false),
				location_type = table.Column<string>("text", nullable: false),
				location_city = table.Column<string>("character varying(32)", maxLength: 32, nullable: true),
				location_state = table.Column<string>("character varying(2)", maxLength: 2, nullable: true),
				contract_type = table.Column<string>("text", nullable: false),
				contract_min_salary = table.Column<decimal>("numeric", nullable: false),
				contract_max_salary = table.Column<decimal>("numeric", nullable: false),
				created_at = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
				requirements = table.Column<string>("jsonb", nullable: true)
			},
			constraints: table => { table.PrimaryKey("pk_job_opportunities", x => x.id); });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(
			"job_opportunities",
			"jobs");
}