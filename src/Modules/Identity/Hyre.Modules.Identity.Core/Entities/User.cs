// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

#endregion

namespace Hyre.Modules.Identity.Core.Entities;

/// <summary>
///   Represents a user in the system.
/// </summary>
public sealed class User : IdentityUser
{
	/// <summary>
	///   Initializes a new instance of the <see cref="User" /> class.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	private User(string email)
	{
		Email = email;
		EmailConfirmed = true;
		UserName = email;
	}

	/// <summary>
	///   Gets or sets the refresh token.
	/// </summary>
	public RefreshToken? RefreshToken { get; private set; }

	/// <summary>
	///   This method will add a refresh token to the user.
	/// </summary>
	/// <param name="refreshToken">The refresh token to be added.</param>
	public void AddRefreshToken(RefreshToken refreshToken) => RefreshToken = refreshToken;

	/// <summary>
	///   Creates a new instance of the <see cref="User" /> class.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	/// <returns>Returns a new instance of the <see cref="User" /> class.</returns>
	public static User Create(string email) => new(email);
}