// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#endregion

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hyre.Modules.Identity.Infrastructure.Migrations;

/// <inheritdoc />
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public partial class AddRefreshToken : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			"identity");

		migrationBuilder.CreateTable(
			"roles",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<string>("text", nullable: false),
				name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				normalized_name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				concurrency_stamp = table.Column<string>("text", nullable: true)
			},
			constraints: table => table.PrimaryKey("pk_roles", x => x.id));

		migrationBuilder.CreateTable(
			"users",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<string>("text", nullable: false),
				refresh_token = table.Column<string>("text", nullable: true),
				refresh_token_expires_at = table.Column<DateTime>("timestamp with time zone", nullable: true),
				user_name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				normalized_user_name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				email = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				normalized_email = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				email_confirmed = table.Column<bool>("boolean", nullable: false),
				password_hash = table.Column<string>("text", nullable: true),
				security_stamp = table.Column<string>("text", nullable: true),
				concurrency_stamp = table.Column<string>("text", nullable: true),
				phone_number = table.Column<string>("text", nullable: true),
				phone_number_confirmed = table.Column<bool>("boolean", nullable: false),
				two_factor_enabled = table.Column<bool>("boolean", nullable: false),
				lockout_end = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true),
				lockout_enabled = table.Column<bool>("boolean", nullable: false),
				access_failed_count = table.Column<int>("integer", nullable: false)
			},
			constraints: table => table.PrimaryKey("pk_users", x => x.id));

		migrationBuilder.CreateTable(
			"role_claims",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<int>("integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				role_id = table.Column<string>("text", nullable: false),
				claim_type = table.Column<string>("text", nullable: true),
				claim_value = table.Column<string>("text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_role_claims", x => x.id);
				table.ForeignKey(
					"fk_role_claims_asp_net_roles_role_id",
					x => x.role_id,
					principalSchema: "identity",
					principalTable: "roles",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			"user_claims",
			schema: "identity",
			columns: table => new
			{
				id = table.Column<int>("integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				user_id = table.Column<string>("text", nullable: false),
				claim_type = table.Column<string>("text", nullable: true),
				claim_value = table.Column<string>("text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_claims", x => x.id);
				table.ForeignKey(
					"fk_user_claims_asp_net_users_user_id",
					x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			"user_logins",
			schema: "identity",
			columns: table => new
			{
				login_provider = table.Column<string>("text", nullable: false),
				provider_key = table.Column<string>("text", nullable: false),
				provider_display_name = table.Column<string>("text", nullable: true),
				user_id = table.Column<string>("text", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
				table.ForeignKey(
					"fk_user_logins_users_user_id",
					x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			"user_roles",
			schema: "identity",
			columns: table => new
			{
				user_id = table.Column<string>("text", nullable: false),
				role_id = table.Column<string>("text", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
				table.ForeignKey(
					"fk_user_roles_roles_role_id",
					x => x.role_id,
					principalSchema: "identity",
					principalTable: "roles",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					"fk_user_roles_users_user_id",
					x => x.user_id,
					principalSchema: "identity",
					principalTable: "users",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			"user_tokens",
			schema: "identity",
			columns: table => new
			{
				user_id = table.Column<string>("text", nullable: false),
				login_provider = table.Column<string>("text", nullable: false),
				name = table.Column<string>("text", nullable: false),
				value = table.Column<string>("text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
				table.ForeignKey(
					"fk_user_tokens_users_user_id",
					x => x.user_id,
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
				{ "35c32022-97e6-499c-b704-2a89a4f23527", null, "Administrator", "ADMINISTRATOR" },
				{ "85bce6eb-bc3c-46f7-8056-5304b485fbac", null, "Candidate", "CANDIDATE" },
				{ "a9f508f1-8130-4eab-8f15-699069231e4e", null, "Employee", "EMPLOYEE" }
			});

		migrationBuilder.CreateIndex(
			"ix_role_claims_role_id",
			schema: "identity",
			table: "role_claims",
			column: "role_id");

		migrationBuilder.CreateIndex(
			"RoleNameIndex",
			schema: "identity",
			table: "roles",
			column: "normalized_name",
			unique: true);

		migrationBuilder.CreateIndex(
			"ix_user_claims_user_id",
			schema: "identity",
			table: "user_claims",
			column: "user_id");

		migrationBuilder.CreateIndex(
			"ix_user_logins_user_id",
			schema: "identity",
			table: "user_logins",
			column: "user_id");

		migrationBuilder.CreateIndex(
			"ix_user_roles_role_id",
			schema: "identity",
			table: "user_roles",
			column: "role_id");

		migrationBuilder.CreateIndex(
			"EmailIndex",
			schema: "identity",
			table: "users",
			column: "normalized_email");

		migrationBuilder.CreateIndex(
			"UserNameIndex",
			schema: "identity",
			table: "users",
			column: "normalized_user_name",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"role_claims",
			"identity");

		migrationBuilder.DropTable(
			"user_claims",
			"identity");

		migrationBuilder.DropTable(
			"user_logins",
			"identity");

		migrationBuilder.DropTable(
			"user_roles",
			"identity");

		migrationBuilder.DropTable(
			"user_tokens",
			"identity");

		migrationBuilder.DropTable(
			"roles",
			"identity");

		migrationBuilder.DropTable(
			"users",
			"identity");
	}
}