// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.IdentityModel.Tokens.Jwt;
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

	/// <summary>
	///   This method will validate the user.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	/// <param name="password">The password of the user.</param>
	/// <returns>Returns true if the user is valid, otherwise false.</returns>
	Task<bool> ValidateAsync(string email, string password);

	/// <summary>
	///   This method will create the access token.
	/// </summary>
	/// <param name="populateExpiresAt">
	///   A value indicating whether to populate the <see cref="JwtSecurityToken.ValidTo" />
	///   property.
	/// </param>
	/// <returns>Returns the access token and the refresh token.</returns>
	Task<(string accessToken, string refreshToken)> CreateAccessTokenAsync(bool populateExpiresAt);

	/// <summary>
	///   This method will create the refresh token.
	/// </summary>
	/// <param name="accessToken">The access token.</param>
	/// <param name="refreshToken">The refresh token.</param>
	/// <returns>Returns the access token and the refresh token.</returns>
	Task<(string accessToken, string refreshToken)> CreateRefreshTokenAsync(string accessToken, string refreshToken);
}