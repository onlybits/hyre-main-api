// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Entities;

#endregion

namespace Hyre.Modules.Identity.Core.Constants;

/// <summary>
///   Error messages that are used for the <see cref="User" /> entity.
/// </summary>
public abstract class UserErrorMessages
{
	#region Application

	/// <summary>
	///   Used when the user already exists.
	/// </summary>
	public const string AlreadyExists = "O usuário com o e-mail informado já existe.";

	#endregion
}