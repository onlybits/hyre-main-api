// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Diagnostics.CodeAnalysis;
using Bogus;
using Hyre.Modules.Jobs.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Common;

/// <summary>
///   The base fixture for the integration tests.
/// </summary>
[SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope")]
public abstract class BaseFixture : IClassFixture<IntegrationTestsWebApplicationFactory>
{
	private readonly IntegrationTestsWebApplicationFactory _factory;

	/// <summary>
	///   Initializes a new instance of the <see cref="BaseFixture" /> class.
	/// </summary>
	/// <param name="factory">The integration tests web application factory.</param>
	protected BaseFixture(IntegrationTestsWebApplicationFactory factory) => _factory = factory;

	/// <summary>
	///   This faker is used to generate random data for the tests.
	/// </summary>
	internal Faker Faker { get; } = new("pt_BR");

	/// <summary>
	///   Creates a new instance of the <see cref="JobsRepositoryContext" />.
	/// </summary>
	/// <returns>It will return a new instance of the <see cref="JobsRepositoryContext" />.</returns>
	internal JobsRepositoryContext CreateRepositoryContext()
	{
		var serviceScope = _factory.Services.CreateScope();
		var repositoryContext = serviceScope.ServiceProvider.GetRequiredService<JobsRepositoryContext>();

		return repositoryContext;
	}
}