// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Infrastructure.Postgres;

/// <summary>
///   This is the base options for the Postgres database.
/// </summary>
internal sealed record PostgresOptions
{
	/// <summary>
	///   Gets the section name for the Postgres database.
	/// </summary>
	public const string SectionName = "Postgres";

	/// <summary>
	///   Gets the connection string for the Postgres database.
	/// </summary>
	public string? ConnectionString { get; init; }
}