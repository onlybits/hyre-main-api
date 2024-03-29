﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public partial class AddInitial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			"jobs");

		migrationBuilder.CreateTable(
			"candidates",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>("uuid", nullable: false),
				first_name = table.Column<string>("character varying(32)", maxLength: 32, nullable: false),
				middle_name = table.Column<string>("character varying(32)", maxLength: 32, nullable: false),
				last_name = table.Column<string>("character varying(32)", maxLength: 32, nullable: false),
				email = table.Column<string>("text", nullable: false),
				document = table.Column<string>("text", nullable: false),
				date_of_birth = table.Column<DateOnly>("date", nullable: false),
				seniority = table.Column<int>("integer", nullable: false),
				disability_hearing = table.Column<bool>("boolean", nullable: true),
				disability_vision = table.Column<bool>("boolean", nullable: true),
				disability_intellectual = table.Column<bool>("boolean", nullable: true),
				disability_physical = table.Column<bool>("boolean", nullable: true),
				gender = table.Column<int>("integer", nullable: false),
				phone_area_code = table.Column<string>("character varying(2)", maxLength: 2, nullable: false),
				phone_number = table.Column<string>("character varying(9)", maxLength: 9, nullable: false),
				address_country = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
				address_state = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
				address_city = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
				address_neighborhood = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
				address_complement = table.Column<string>("character varying(100)", maxLength: 100, nullable: true),
				address_number = table.Column<int>("integer", nullable: false),
				address_zip_code = table.Column<string>("character varying(8)", maxLength: 8, nullable: false),
				social_network_linkedin = table.Column<string>("character varying(32)", maxLength: 32, nullable: true),
				social_network_github = table.Column<string>("character varying(32)", maxLength: 32, nullable: true),
				created_at = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
				educations = table.Column<string>("jsonb", nullable: true),
				experiences = table.Column<string>("jsonb", nullable: true),
				languages = table.Column<string>("jsonb", nullable: true)
			},
			constraints: table => table.PrimaryKey("pk_candidates", x => x.id));

		migrationBuilder.CreateTable(
			"inbox_state",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<long>("bigint", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				message_id = table.Column<Guid>("uuid", nullable: false),
				consumer_id = table.Column<Guid>("uuid", nullable: false),
				lock_id = table.Column<Guid>("uuid", nullable: false),
				row_version = table.Column<byte[]>("bytea", rowVersion: true, nullable: true),
				received = table.Column<DateTime>("timestamp with time zone", nullable: false),
				receive_count = table.Column<int>("integer", nullable: false),
				expiration_time = table.Column<DateTime>("timestamp with time zone", nullable: true),
				consumed = table.Column<DateTime>("timestamp with time zone", nullable: true),
				delivered = table.Column<DateTime>("timestamp with time zone", nullable: true),
				last_sequence_number = table.Column<long>("bigint", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_inbox_state", x => x.id);
				table.UniqueConstraint("ak_inbox_state_message_id_consumer_id", x => new { x.message_id, x.consumer_id });
			});

		migrationBuilder.CreateTable(
			"job_opportunities",
			schema: "jobs",
			columns: table => new
			{
				id = table.Column<Guid>("uuid", nullable: false),
				name = table.Column<string>("character varying(64)", maxLength: 64, nullable: false),
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
			constraints: table => table.PrimaryKey("pk_job_opportunities", x => x.id));

		migrationBuilder.CreateTable(
			"outbox_message",
			schema: "jobs",
			columns: table => new
			{
				sequence_number = table.Column<long>("bigint", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				enqueue_time = table.Column<DateTime>("timestamp with time zone", nullable: true),
				sent_time = table.Column<DateTime>("timestamp with time zone", nullable: false),
				headers = table.Column<string>("text", nullable: true),
				properties = table.Column<string>("text", nullable: true),
				inbox_message_id = table.Column<Guid>("uuid", nullable: true),
				inbox_consumer_id = table.Column<Guid>("uuid", nullable: true),
				outbox_id = table.Column<Guid>("uuid", nullable: true),
				message_id = table.Column<Guid>("uuid", nullable: false),
				content_type = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
				message_type = table.Column<string>("text", nullable: false),
				body = table.Column<string>("text", nullable: false),
				conversation_id = table.Column<Guid>("uuid", nullable: true),
				correlation_id = table.Column<Guid>("uuid", nullable: true),
				initiator_id = table.Column<Guid>("uuid", nullable: true),
				request_id = table.Column<Guid>("uuid", nullable: true),
				source_address = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				destination_address = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				response_address = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				fault_address = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
				expiration_time = table.Column<DateTime>("timestamp with time zone", nullable: true)
			},
			constraints: table => table.PrimaryKey("pk_outbox_message", x => x.sequence_number));

		migrationBuilder.CreateTable(
			"outbox_state",
			schema: "jobs",
			columns: table => new
			{
				outbox_id = table.Column<Guid>("uuid", nullable: false),
				lock_id = table.Column<Guid>("uuid", nullable: false),
				row_version = table.Column<byte[]>("bytea", rowVersion: true, nullable: true),
				created = table.Column<DateTime>("timestamp with time zone", nullable: false),
				delivered = table.Column<DateTime>("timestamp with time zone", nullable: true),
				last_sequence_number = table.Column<long>("bigint", nullable: true)
			},
			constraints: table => table.PrimaryKey("pk_outbox_state", x => x.outbox_id));

		migrationBuilder.CreateTable(
			"candidate_job_opportunity",
			schema: "jobs",
			columns: table => new
			{
				candidates_id = table.Column<Guid>("uuid", nullable: false),
				job_opportunities_id = table.Column<Guid>("uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("pk_candidate_job_opportunity", x => new { x.candidates_id, x.job_opportunities_id });
				table.ForeignKey(
					"fk_candidate_job_opportunity_candidates_candidates_id",
					x => x.candidates_id,
					principalSchema: "jobs",
					principalTable: "candidates",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					"fk_candidate_job_opportunity_job_opportunities_job_opportuniti",
					x => x.job_opportunities_id,
					principalSchema: "jobs",
					principalTable: "job_opportunities",
					principalColumn: "id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"ix_candidate_job_opportunity_job_opportunities_id",
			schema: "jobs",
			table: "candidate_job_opportunity",
			column: "job_opportunities_id");

		migrationBuilder.CreateIndex(
			"ix_candidates_email",
			schema: "jobs",
			table: "candidates",
			column: "email",
			unique: true);

		migrationBuilder.CreateIndex(
			"ix_inbox_state_delivered",
			schema: "jobs",
			table: "inbox_state",
			column: "delivered");

		migrationBuilder.CreateIndex(
			"ix_outbox_message_enqueue_time",
			schema: "jobs",
			table: "outbox_message",
			column: "enqueue_time");

		migrationBuilder.CreateIndex(
			"ix_outbox_message_expiration_time",
			schema: "jobs",
			table: "outbox_message",
			column: "expiration_time");

		migrationBuilder.CreateIndex(
			"ix_outbox_message_inbox_message_id_inbox_consumer_id_sequence_",
			schema: "jobs",
			table: "outbox_message",
			columns: new[] { "inbox_message_id", "inbox_consumer_id", "sequence_number" },
			unique: true);

		migrationBuilder.CreateIndex(
			"ix_outbox_message_outbox_id_sequence_number",
			schema: "jobs",
			table: "outbox_message",
			columns: new[] { "outbox_id", "sequence_number" },
			unique: true);

		migrationBuilder.CreateIndex(
			"ix_outbox_state_created",
			schema: "jobs",
			table: "outbox_state",
			column: "created");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"candidate_job_opportunity",
			"jobs");

		migrationBuilder.DropTable(
			"inbox_state",
			"jobs");

		migrationBuilder.DropTable(
			"outbox_message",
			"jobs");

		migrationBuilder.DropTable(
			"outbox_state",
			"jobs");

		migrationBuilder.DropTable(
			"candidates",
			"jobs");

		migrationBuilder.DropTable(
			"job_opportunities",
			"jobs");
	}
}