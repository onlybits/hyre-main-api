// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Identity.Core.ValueObjects;

/// <summary>
///   Represents a refresh token for the user.
/// </summary>
public sealed record RefreshToken : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="RefreshToken" /> class.
	/// </summary>
	/// <param name="value">The value of the refresh token.</param>
	public RefreshToken(string value)
	{
		Value = value;
		ExpiresAt = DateTime.UtcNow.AddDays(7);
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="RefreshToken" /> class.
	/// </summary>
	/// <param name="value">The value of the refresh token.</param>
	/// <param name="expiresAt">The date and time when the refresh token expires.</param>
	public RefreshToken(string value, DateTime expiresAt)
	{
		Value = value;
		ExpiresAt = expiresAt;
	}

	/// <summary>
	///   Gets the value of the refresh token.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   Gets the date and time when the refresh token expires.
	/// </summary>
	public DateTime ExpiresAt { get; }
}