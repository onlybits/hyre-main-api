// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Text.Json;

#endregion

namespace Hyre.Bootstrapper.Errors;

/// <summary>
///   This class is used to return error details in the response body.
/// </summary>
public sealed class ErrorDetails
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
		WriteIndented = true
	};

	/// <summary>
	///   Gets or sets the status code of the response.
	/// </summary>
	public int Status { get; set; }

	/// <summary>
	///   Gets or sets the error message of the response.
	/// </summary>
	public string? Message { get; set; }

	/// <summary>
	///   Used to return the validation errors in the response.
	/// </summary>
	/// <returns></returns>
	public override string ToString() => JsonSerializer.Serialize(this, JsonOptions);
}