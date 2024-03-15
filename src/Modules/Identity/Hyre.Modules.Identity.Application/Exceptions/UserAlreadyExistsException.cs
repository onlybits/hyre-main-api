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