// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Bootstrapper.Errors;
using Hyre.Bootstrapper.Extensions;
using Hyre.Modules.Identity.API;
using Hyre.Shared.Infrastructure;

#endregion

var builder = WebApplication.CreateBuilder(args);
{
	_ = builder.Services.AddCorsConfiguration();
	_ = builder.Services.AddSwaggerConfiguration();
	_ = builder.Services.AddRabbitMqConfiguration();
	_ = builder.Services.AddModularInfrastructure();
	_ = builder.Services.AddModulesConfiguration();
	_ = builder.Services.AddOptionsConfiguration(builder.Configuration);
	_ = builder.Services.AddIdentityConfiguration();
	_ = builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
	builder.Services.AddControllersConfiguration();
}

var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		_ = app.AddSwagger();
	}

	_ = app.UseExceptionHandler(_ => { });
	_ = app.UseHttpsRedirection();
	_ = app.UseCors("Hyre.Cors");
	_ = app.UseModules();
	_ = app.UseAuthentication();
	_ = app.UseAuthorization();

	_ = app.MapControllers();
	app.Run();
}