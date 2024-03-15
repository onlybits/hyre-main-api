// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Bootstrapper;
using Hyre.Modules.Jobs.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Common;

/// <summary>
///   This class is responsible for creating a new test server for integration tests.
/// </summary>
public sealed class IntegrationTestsWebApplicationFactory : WebApplicationFactory<IBootstrapperMarker>, IAsyncLifetime
{
	/// <summary>
	///   Creates a new instance of the database in a separate container.
	/// </summary>
	private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
		.WithImage("postgres:latest")
		.WithDatabase("hyre-db")
		.WithUsername("postgres")
		.WithPassword("postgres")
		.Build();

	/// <summary>
	///   Runs before the first test in the test class to perform any setup.
	/// </summary>
	public async Task InitializeAsync() => await _dbContainer.StartAsync();

	/// <summary>
	///   Runs after the last test in the test class to perform any cleanup.
	/// </summary>
	public new async Task DisposeAsync()
	{
		await _dbContainer.StopAsync();
		await _dbContainer.DisposeAsync();
	}

	/// <summary>
	///   Configures the <see cref="IWebHostBuilder" /> to be used to create a test server.
	/// </summary>
	/// <param name="builder">The <see cref="IWebHostBuilder" /> to configure.</param>
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		base.ConfigureWebHost(builder);

		_ = builder.ConfigureTestServices(services =>
		{
			var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<JobsRepositoryContext>));
			if (descriptor is not null)
			{
				_ = services.Remove(descriptor);
			}

			_ = services.AddDbContext<JobsRepositoryContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString())
				.UseSnakeCaseNamingConvention());
		});

		_ = builder.UseTestServer();
	}
}