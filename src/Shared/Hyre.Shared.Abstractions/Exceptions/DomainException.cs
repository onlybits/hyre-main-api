﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a domain rule is violated.
/// </summary>
public abstract class DomainException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="DomainException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	protected DomainException(string message) : base(message)
	{
	}
}