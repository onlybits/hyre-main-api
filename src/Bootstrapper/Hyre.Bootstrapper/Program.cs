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
builder.Services
	.AddCorsConfiguration()
	.AddSwaggerConfiguration()
	.AddRabbitMqConfiguration()
	.AddModularInfrastructure()
	.AddModulesConfiguration()
	.AddIdentityConfiguration()
	.AddExceptionHandler<GlobalExceptionHandler>()
	.AddControllersConfiguration();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	_ = app.AddSwagger();
}

_ = app.UseExceptionHandler(_ => { });
_ = app.UseHttpsRedirection()
	.UseCors("Hyre.Cors")
	.UseModules()
	.UseAuthentication()
	.UseAuthorization();

_ = app.MapControllers();
app.Run();