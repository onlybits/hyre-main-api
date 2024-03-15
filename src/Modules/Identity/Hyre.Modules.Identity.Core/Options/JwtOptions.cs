// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Identity.Core.Options;

/// <summary>
///   Represents the JWT options.
/// </summary>
public sealed class JwtOptions
{
	/// <summary>
	///   Gets or sets the section name.
	/// </summary>
	public const string Name = "Identity";

	/// <summary>
	///   Gets or sets the issuer.
	/// </summary>
	public string Issuer { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the audience.
	/// </summary>
	public string Audience { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the secret.
	/// </summary>
	public string Secret { get; set; } = string.Empty;

	/// <summary>
	///   Gets or sets the expiration.
	/// </summary>
	public int Expiration { get; set; }
}