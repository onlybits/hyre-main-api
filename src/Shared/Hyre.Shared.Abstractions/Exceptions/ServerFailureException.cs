// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a server failure occurs.
/// </summary>
public abstract class ServerFailureException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="ServerFailureException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	protected ServerFailureException(string message) : base(message)
	{
	}
}