// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Bootstrapper.Extensions;
using Hyre.Shared.Infrastructure;

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Services
	.AddCorsConfiguration()
	.AddSwaggerConfiguration()
	.AddSharedInfrastructure()
	.AddControllersConfiguration();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	_ = app.AddSwagger();
}

_ = app.UseExceptionHandler(_ => { });
_ = app.UseHttpsRedirection()
	.UseCors("Hyre.Cors");
_ = app.MapControllers();
app.Run();