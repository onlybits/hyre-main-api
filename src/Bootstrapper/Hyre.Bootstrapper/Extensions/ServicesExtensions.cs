// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Infrastructure.API;
using Hyre.Shared.Infrastructure.Serialization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This class contains the extension methods for the <see cref="IServiceCollection" /> interface.
/// </summary>
public static class ServicesExtensions
{
	/// <summary>
	///   This will configure the CORS policy for the application.
	/// </summary>
	/// <param name="services">The service's collection.</param>
	/// <returns>Returns the services collection.</returns>
	public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
		services.AddCors(options => options.AddPolicy("Hyre.Cors",
			builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithExposedHeaders("X-Pagination")));

	/// <summary>
	///   This will configure the controllers for the application.
	/// </summary>
	/// <param name="services">The service's collection.</param>
	/// <returns>It will return the same services collection.</returns>
	public static void AddControllersConfiguration(this IServiceCollection services)
	{
		_ = services.AddEndpointsApiExplorer();
		_ = services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
		_ = services.AddControllers(config =>
			{
				config.RespectBrowserAcceptHeader = true;
				config.ReturnHttpNotAcceptable = true;
			})
			.ConfigureApplicationPartManager(manager => manager.FeatureProviders.Add(new InternalControllerFeatureProvider()))
			.AddJsonOptions(opt =>
			{
				var defaultOptions = CustomJsonOptions.GetDefault();
				opt.JsonSerializerOptions.PropertyNamingPolicy = defaultOptions.PropertyNamingPolicy;
				opt.JsonSerializerOptions.WriteIndented = defaultOptions.WriteIndented;
			});
	}
}