// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			"jobs");

		migrationBuilder.CreateTable(
			"JobOpportunities",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>("uuid", nullable: false),
				name = table.Column<string>("character varying(32)", maxLength: 32, nullable: false)
			},
			constraints: table => { table.PrimaryKey("pk_job_opportunities", x => x.id); });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(
			"JobOpportunities",
			"jobs");
}