// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Entities;

#endregion

namespace Hyre.Modules.Identity.Application.Services;

/// <summary>
///   This is the service that will handle the identity operations.
/// </summary>
public interface IIdentityService
{
	/// <summary>
	///   This method will create a new user in the database.
	/// </summary>
	/// <param name="user">The user to be created.</param>
	/// <param name="password">The password of the user.</param>
	/// <param name="role">The role of the user.</param>
	/// <returns>Returns true if the user was created successfully, otherwise false.</returns>
	Task<bool> CreateUserAsync(User user, string password, string role);

	/// <summary>
	///   This method will check if the user exists in the database.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	/// <returns>Returns true if the user exists, otherwise false.</returns>
	Task<bool> UserExistsAsync(string email);
}