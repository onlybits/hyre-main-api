// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Infrastructure.Messaging;

/// <summary>
///   Represents the RabbitMq options.
/// </summary>
public sealed record RabbitMqOptions
{
	/// <summary>
	///   Gets or sets the section name.
	/// </summary>
	public const string Name = "RabbitMq";

	/// <summary>
	///   Gets or sets the host.
	/// </summary>
	public string Host { get; init; } = string.Empty;

	/// <summary>
	///   Gets or sets the port.
	/// </summary>
	public string Username { get; init; } = string.Empty;

	/// <summary>
	///   Gets or sets the password.
	/// </summary>
	public string Password { get; init; } = string.Empty;
}