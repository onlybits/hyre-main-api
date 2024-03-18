// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a authentication error occurs.
/// </summary>
public abstract class AuthenticationException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="AuthenticationException" /> class.
	/// </summary>
	/// <param name="message"></param>
	protected AuthenticationException(string message) : base(message)
	{
	}
}