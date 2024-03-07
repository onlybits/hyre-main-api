using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hyre.Modules.Jobs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "contract_max_salary",
                schema: "jobs",
                table: "job_opportunities",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "contract_min_salary",
                schema: "jobs",
                table: "job_opportunities",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "contract_type",
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
                name: "contract_max_salary",
                schema: "jobs",
                table: "job_opportunities");

            migrationBuilder.DropColumn(
                name: "contract_min_salary",
                schema: "jobs",
                table: "job_opportunities");

            migrationBuilder.DropColumn(
                name: "contract_type",
                schema: "jobs",
                table: "job_opportunities");
        }
    }
}
