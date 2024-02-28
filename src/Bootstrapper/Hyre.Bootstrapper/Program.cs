// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Infrastructure;

#endregion

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddSharedInfrastructure();
	builder.Services.AddControllers();
}

var app = builder.Build();
{
	app.MapControllers();
	app.Run();
}