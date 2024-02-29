// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Text.Json;

#endregion

namespace Hyre.Shared.Infrastructure.Serialization;

/// <summary>
///   Represents the options for JSON serialization.
/// </summary>
public abstract class CustomJsonOptions
{
	/// <summary>
	///   Gets the default options for JSON serialization.
	/// </summary>
	/// <returns>It will return the default options for JSON serialization.</returns>
	public static JsonSerializerOptions GetDefault() => new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
		WriteIndented = true
	};
}