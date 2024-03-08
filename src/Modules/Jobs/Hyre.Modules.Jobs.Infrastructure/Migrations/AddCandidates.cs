using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddCandidates : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "candidates",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>(type: "uuid", nullable: false),
				first_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
				middle_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
				last_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
				job_opportunity_id = table.Column<Guid>(type: "uuid", nullable: false),
				created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_candidates", x => x.id);
				table.ForeignKey(
					name: "fk_candidates_job_opportunities_job_opportunity_id",
					column: x => x.job_opportunity_id,
					principalSchema: "jobs",
					principalTable: "job_opportunities",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "ix_candidates_job_opportunity_id",
			schema: "jobs",
			table: "candidates",
			column: "job_opportunity_id");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropTable(
			name: "candidates",
			schema: "jobs");
}