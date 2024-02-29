// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Reflection;
using Hyre.Modules.Jobs.API;
using Microsoft.OpenApi.Models;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This is an extension class that contains the documentation's configuration.
/// </summary>
public static class DocumentationExtensions
{
	/// <summary>
	///   This configures the swagger to use in the Program.cs
	/// </summary>
	/// <param name="services"></param>
	/// <returns>Returns the service collection.</returns>
	public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services) => services
		.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Hyre API",
				Version = "v1"
			});

			var jobsXmlFile = $"{Assembly.GetAssembly(typeof(IJobsApiMarker))!.GetName().Name}.xml";
			options.IncludeXmlComments(jobsXmlFile);

			options.CustomSchemaIds(type => type.ToString());
		});

	/// <summary>
	///   This tells the application to use the swagger
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>Returns the application builder.</returns>
	public static IApplicationBuilder AddSwagger(this IApplicationBuilder app) => app
		.UseSwagger()
		.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hyre API v1"));
}