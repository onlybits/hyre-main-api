// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a authorization error occurs.
/// </summary>
public abstract class AuthorizationException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="AuthorizationException" /> class.
	/// </summary>
	/// <param name="message"></param>
	protected AuthorizationException(string message) : base(message)
	{
	}
}