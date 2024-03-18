// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Identity.Application.Exceptions;

/// <summary>
///   Exception that is thrown when the user credentials are invalid.
/// </summary>
public sealed class UserInvalidCredentialsException : AuthenticationException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="UserInvalidCredentialsException" /> class.
	/// </summary>
	public UserInvalidCredentialsException() : base(UserErrorMessages.InvalidCredentials)
	{
	}
}