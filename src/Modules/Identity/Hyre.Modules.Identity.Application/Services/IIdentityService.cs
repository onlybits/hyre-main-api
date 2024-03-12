// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

#endregion

namespace Hyre.Modules.Identity.Application.Services;

/// <summary>
///   This is the service that will handle the identity operations.
/// </summary>
internal interface IIdentityService
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

/// <summary>
///   This is service that will handle the identity operations.
/// </summary>
/// <inheritdoc cref="IIdentityService" />
internal sealed class IdentityService : IIdentityService
{
	private readonly UserManager<User> _userManager;

	/// <summary>
	///   Initializes a new instance of the <see cref="IdentityService" /> class.
	/// </summary>
	/// <param name="userManager">The user manager.</param>
	public IdentityService(UserManager<User> userManager) => _userManager = userManager;

	public async Task<bool> CreateUserAsync(User user, string password, string role)
	{
		var result = await _userManager.CreateAsync(user, password);

		if (result.Succeeded)
		{
			_ = await _userManager.AddToRoleAsync(user, role);
		}

		return result.Succeeded;
	}

	public async Task<bool> UserExistsAsync(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		return user != null;
	}
}