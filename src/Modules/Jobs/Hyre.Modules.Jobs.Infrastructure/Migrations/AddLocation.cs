using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyre.Modules.Jobs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "location_city",
                schema: "jobs",
                table: "job_opportunities",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location_state",
                schema: "jobs",
                table: "job_opportunities",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location_type",
                schema: "jobs",
                table: "job_opportunities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location_city",
                schema: "jobs",
                table: "job_opportunities");

            migrationBuilder.DropColumn(
                name: "location_state",
                schema: "jobs",
                table: "job_opportunities");

            migrationBuilder.DropColumn(
                name: "location_type",
                schema: "jobs",
                table: "job_opportunities");
        }
    }
}
