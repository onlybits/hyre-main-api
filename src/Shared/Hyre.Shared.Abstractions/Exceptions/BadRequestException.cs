// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Exceptions;

/// <summary>
///   Base exception that is used when a bad request error occurs.
/// </summary>
public abstract class BadRequestException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="BadRequestException" /> class.
	/// </summary>
	/// <param name="message"></param>
	protected BadRequestException(string message) : base(message)
	{
	}
}