// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Identity.Application.Exceptions;

/// <summary>
///   Exception that is thrown when the JWT secret key was not found in the environment variables.
/// </summary>
public sealed class JwtSecretKeyNotFoundException : Exception
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JwtSecretKeyNotFoundException" /> class.
	/// </summary>
	public JwtSecretKeyNotFoundException() : base("The JWT secret key was not found in the environment variables.")
	{
		//TODO: Add translation in the future
	}
}