using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hyre.Modules.Identity.Infrastructure.Migrations;

/// <inheritdoc />
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public partial class AddInitial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "identity");

		migrationBuilder.CreateTable(
			name: "roles",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<string>(type: "text", nullable: false),
				name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				concurrency_stamp = table.Column<string>(type: "text", nullable: true)
			},
			constraints: table => table.PrimaryKey("pk_roles", x => x.id));

		migrationBuilder.CreateTable(
			name: "users",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<string>(type: "text", nullable: false),
				user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
				email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
				password_hash = table.Column<string>(type: "text", nullable: true),
				security_stamp = table.Column<string>(type: "text", nullable: true),
				concurrency_stamp = table.Column<string>(type: "text", nullable: true),
				phone_number = table.Column<string>(type: "text", nullable: true),
				phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
				two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
				lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
				lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
				access_failed_count = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table => table.PrimaryKey("pk_users", x => x.id));

		migrationBuilder.CreateTable(
			name: "role_claims",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				role_id = table.Column<string>(type: "text", nullable: false),
				claim_type = table.Column<string>(type: "text", nullable: true),
				claim_value = table.Column<string>(type: "text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_role_claims", x => x.id);
				table.ForeignKey(
					name: "fk_role_claims_asp_net_roles_role_id",
					column: x => x.role_id,
					principalSchema: "identity",
					principalTable: "roles",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "user_claims",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				user_id = table.Column<string>(type: "text", nullable: false),
				claim_type = table.Column<string>(type: "text", nullable: true),
				claim_value = table.Column<string>(type: "text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_claims", x => x.id);
				table.ForeignKey(
					name: "fk_user_claims_asp_net_users_user_id",
					column: x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "user_logins",
			schema: "identity",
			columns: table => new
			{
				login_provider = table.Column<string>(type: "text", nullable: false),
				provider_key = table.Column<string>(type: "text", nullable: false),
				provider_display_name = table.Column<string>(type: "text", nullable: true),
				user_id = table.Column<string>(type: "text", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
				table.ForeignKey(
					name: "fk_user_logins_users_user_id",
					column: x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "user_roles",
			schema: "identity",
			columns: table => new
			{
				user_id = table.Column<string>(type: "text", nullable: false),
				role_id = table.Column<string>(type: "text", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
				table.ForeignKey(
					name: "fk_user_roles_roles_role_id",
					column: x => x.role_id,
					principalSchema: "identity",
					principalTable: "roles",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "fk_user_roles_users_user_id",
					column: x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "user_tokens",
			schema: "identity",
			columns: table => new
			{
				user_id = table.Column<string>(type: "text", nullable: false),
				login_provider = table.Column<string>(type: "text", nullable: false),
				name = table.Column<string>(type: "text", nullable: false),
				value = table.Column<string>(type: "text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
				table.ForeignKey(
					name: "fk_user_tokens_users_user_id",
					column: x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.InsertData(
			schema: "identity",
			table: "roles",
			columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
			values: new object[,]
			{
				{ "5a312ae6-f5a8-4f96-b2a9-26d581f998d6", null, "Candidate", "CANDIDATE" },
				{ "6f37df87-6caa-4cd4-a981-70a5b432458f", null, "Administrator", "ADMINISTRATOR" },
				{ "fb04f04b-a13e-40a6-a417-429cf1cefd04", null, "Employee", "EMPLOYEE" }
			});

		migrationBuilder.CreateIndex(
			name: "ix_role_claims_role_id",
			schema: "identity",
			table: "role_claims",
			column: "role_id");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			schema: "identity",
			table: "roles",
			column: "normalized_name",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "ix_user_claims_user_id",
			schema: "identity",
			table: "user_claims",
			column: "user_id");

		migrationBuilder.CreateIndex(
			name: "ix_user_logins_user_id",
			schema: "identity",
			table: "user_logins",
			column: "user_id");

		migrationBuilder.CreateIndex(
			name: "ix_user_roles_role_id",
			schema: "identity",
			table: "user_roles",
			column: "role_id");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			schema: "identity",
			table: "users",
			column: "normalized_email");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			schema: "identity",
			table: "users",
			column: "normalized_user_name",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "role_claims",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "user_claims",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "user_logins",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "user_roles",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "user_tokens",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "roles",
			schema: "identity");

		migrationBuilder.DropTable(
			name: "users",
			schema: "identity");
	}
}