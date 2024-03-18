// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hyre.Modules.Identity.Application.Exceptions;
using Hyre.Modules.Identity.Core.Entities;
using Hyre.Modules.Identity.Core.Options;
using Hyre.Modules.Identity.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Hyre.Modules.Identity.Application.Services;

/// <summary>
///   This is service that will handle the identity operations.
/// </summary>
/// <inheritdoc cref="IIdentityService" />
internal sealed class IdentityService : IIdentityService
{
	private readonly JwtOptions _options;
	private readonly UserManager<User> _userManager;
	private User? _user;

	/// <summary>
	///   Initializes a new instance of the <see cref="IdentityService" /> class.
	/// </summary>
	/// <param name="userManager">The user manager.</param>
	/// <param name="options">The jwt options.</param>
	public IdentityService(UserManager<User> userManager, IOptions<JwtOptions> options)
	{
		_options = options.Value;
		_userManager = userManager;
	}

	/// <summary>
	///   This method will create a new user in the database.
	/// </summary>
	/// <param name="user">The user to be created.</param>
	/// <param name="password">The password of the user.</param>
	/// <param name="role">The role of the user.</param>
	/// <returns>Returns true if the user was created successfully, otherwise false.</returns>
	public async Task<bool> CreateUserAsync(User user, string password, string role)
	{
		var result = await _userManager.CreateAsync(user, password);

		if (result.Succeeded)
		{
			_ = await _userManager.AddToRoleAsync(user, role);
		}

		return result.Succeeded;
	}

	/// <summary>
	///   This method will check if the user exists in the database.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	/// <returns>Returns true if the user exists, otherwise false.</returns>
	public async Task<bool> UserExistsAsync(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		return user != null;
	}

	/// <summary>
	///   This method will validate the user.
	/// </summary>
	/// <param name="email">The email of the user.</param>
	/// <param name="password">The password of the user.</param>
	/// <returns>Returns true if the user is valid, otherwise false.</returns>
	public async Task<bool> ValidateAsync(string email, string password)
	{
		_user = await _userManager.FindByEmailAsync(email);

		if (_user is null)
		{
			return false;
		}

		var userIsValid = await _userManager.CheckPasswordAsync(_user, password);
		return userIsValid;
	}

	/// <summary>
	///   This method will create the access token.
	/// </summary>
	/// <param name="updateRefreshTokenExpiresAt">If the refresh token expires at should be updated.</param>
	/// <returns>Returns the access token.</returns>
	public async Task<(string accessToken, string refreshToken)> CreateAccessTokenAsync(bool updateRefreshTokenExpiresAt)
	{
		if (_user is null)
		{
			throw new UserInvalidCredentialsException();
		}

		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims();
		var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

		var accessTokenValue = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		var refreshTokenValue = GenerateRefreshToken();

		if (updateRefreshTokenExpiresAt)
		{
			var refreshToken = new RefreshToken(refreshTokenValue);
			_user.AddRefreshToken(refreshToken);
		}
		else
		{
			var currentRefreshTokenExpiresAt = _user.RefreshToken?.ExpiresAt ?? DateTime.UtcNow;
			var refreshToken = new RefreshToken(refreshTokenValue, currentRefreshTokenExpiresAt);
			_user.AddRefreshToken(refreshToken);
		}

		await _userManager.UpdateAsync(_user);
		return (accessTokenValue, refreshTokenValue);
	}

	public async Task<(string accessToken, string refreshToken)> CreateRefreshTokenAsync(string accessToken, string refreshToken)
	{
		var principal = GetPrincipalFromExpiredToken(accessToken);
		var user = await _userManager.FindByNameAsync(principal.Identity?.Name!);
		if (user is null || user.RefreshToken?.Value != refreshToken || user.RefreshToken?.ExpiresAt < DateTime.UtcNow)
		{
			throw new RefreshTokenInvalidException();
		}

		_user = user;

		var (newAccessToken, newRefreshToken) = await CreateAccessTokenAsync(false);
		return (newAccessToken, newRefreshToken);
	}

	private SigningCredentials GetSigningCredentials()
	{
		var key = Encoding.UTF8.GetBytes(_options.Secret);
		var secret = new SymmetricSecurityKey(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	private async Task<List<Claim>> GetClaims()
	{
		if (_user is null)
		{
			throw new UserInvalidCredentialsException();
		}

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, _user.UserName!)
		};

		var roles = await _userManager.GetRolesAsync(_user);
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		return claims;
	}

	private JwtSecurityToken GenerateTokenOptions(
		SigningCredentials signingCredentials,
		IEnumerable<Claim> claims)
	{
		var tokenOptions = new JwtSecurityToken
		(
			_options.Issuer,
			_options.Audience,
			claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(_options.Expiration)),
			signingCredentials: signingCredentials
		);

		return tokenOptions;
	}

	private static string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}

	private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
			ValidateLifetime = true,
			ValidIssuer = _options.Issuer,
			ValidAudience = _options.Audience
		};
		var tokenHandler = new JwtSecurityTokenHandler();
		var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
		if (securityToken is not JwtSecurityToken jwtSecurityToken
		    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
			    StringComparison.OrdinalIgnoreCase))
		{
			throw new RefreshTokenInvalidException();
		}

		return principal;
	}
}