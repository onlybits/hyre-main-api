// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace Hyre.Shared.Infrastructure.Postgres;

/// <summary>
///   This class is used to automatically initialize the modules' Postgres contexts.
/// </summary>
/// <remarks>
///   Each module should have its own Postgres context, and this class will initialize them all.
/// </remarks>
internal sealed class DbContextAppInitializer : IHostedService
{
	private readonly ILoggerManager _logger;
	private readonly IServiceProvider _serviceProvider;

	public DbContextAppInitializer(IServiceProvider serviceProvider, ILoggerManager logger)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		var dbContextTypes = AppDomain.CurrentDomain
			.GetAssemblies()
			.SelectMany(x => x.GetTypes())
			.Where(x => typeof(DbContext).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false } && x != typeof(DbContext));

		using var scope = _serviceProvider.CreateScope();

		foreach (var dbContextType in dbContextTypes)
		{
			if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext)
			{
				_logger.LogError("The {Name} could not be resolved.", dbContextType.Name);
				continue;
			}

			_logger.LogInfo("The {Name} is being initialized.", dbContextType.Name);
			await dbContext.Database.MigrateAsync(cancellationToken);
		}
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}