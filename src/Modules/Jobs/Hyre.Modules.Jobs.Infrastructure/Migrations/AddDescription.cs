// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

#region

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDescription : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder) =>
		migrationBuilder.AddColumn<string>(
			"description",
			schema: "jobs",
			table: "JobOpportunities",
			type: "character varying(500)",
			maxLength: 500,
			nullable: false,
			defaultValue: "");

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) =>
		migrationBuilder.DropColumn(
			"description",
			schema: "jobs",
			table: "JobOpportunities");
}