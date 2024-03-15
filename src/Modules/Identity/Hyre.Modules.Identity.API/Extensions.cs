// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Text;
using Hyre.Modules.Identity.Application.Exceptions;
using Hyre.Modules.Identity.Core.Entities;
using Hyre.Modules.Identity.Core.Options;
using Hyre.Modules.Identity.Infrastructure;
using Hyre.Shared.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Hyre.Modules.Identity.API;

/// <summary>
///   This class contains the extensions for the identity.
/// </summary>
internal static class Extensions
{
	/// <summary>
	///   This method adds the identity configuration to the services.
	/// </summary>
	/// <param name="services">The services collection.</param>
	/// <returns>Returns the services collection.</returns>
	public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
	{
		_ = services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;
				options.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<IdentityRepositoryContext>()
			.AddDefaultTokenProviders();

		_ = services.ConfigureJwt();
		return services;
	}

	/// <summary>
	///   This method configures the JWT settings.
	/// </summary>
	/// <param name="services">The services collection.</param>
	/// <returns>Returns the services collection.</returns>
	private static IServiceCollection ConfigureJwt(this IServiceCollection services)
	{
		var jwtOptions = services.GetOptions<JwtOptions>(JwtOptions.Name);

		if (jwtOptions.Secret is null)
		{
			throw new JwtSecretKeyNotFoundException();
		}

		_ = services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = jwtOptions.Issuer,
			ValidAudience = jwtOptions.Audience,

			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,

			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
		});

		return services;
	}
}