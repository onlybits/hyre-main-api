// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a already exists.
/// </summary>
/// <remarks>
///   This is mainly used in the project to indicate that a resource already exists in the database.
/// </remarks>
public abstract class ConflictException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="ConflictException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	protected ConflictException(string message) : base(message)
	{
	}
}