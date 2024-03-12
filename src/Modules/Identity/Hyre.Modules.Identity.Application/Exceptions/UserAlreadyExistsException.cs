// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Identity.Application.Exceptions;

/// <summary>
///   Exception that is thrown when a user already exists in the system.
/// </summary>
public sealed class UserAlreadyExistsException : ConflictException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="UserAlreadyExistsException" /> class.
	/// </summary>
	public UserAlreadyExistsException() : base(UserErrorMessages.AlreadyExists)
	{
	}
}

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