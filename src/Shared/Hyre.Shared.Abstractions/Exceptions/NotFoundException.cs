// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a resource is not found.
/// </summary>
public abstract class NotFoundException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotFoundException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	protected NotFoundException(string message) : base(message)
	{
	}
}