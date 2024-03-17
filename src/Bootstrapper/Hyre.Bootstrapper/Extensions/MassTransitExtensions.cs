// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Modules.Notifications.Application;
using Hyre.Shared.Infrastructure;
using Hyre.Shared.Infrastructure.Messaging;
using MassTransit;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This class contains extension methods for MassTransit.
/// </summary>
public static class MassTransitExtensions
{
	/// <summary>
	///   This method adds the RabbitMq configuration to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>Returns the service collection with the RabbitMq configuration.</returns>
	public static IServiceCollection AddRabbitMqConfiguration(this IServiceCollection services)
	{
		var rabbitMqOptions = services.GetOptions<RabbitMqOptions>(RabbitMqOptions.Name);

		_ = services.AddMassTransit(busConfigurator =>
		{
			busConfigurator.SetKebabCaseEndpointNameFormatter();
			busConfigurator.AddConsumers();
			busConfigurator.AddOutbox();

			busConfigurator.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(new Uri(rabbitMqOptions.Host), h =>
				{
					h.Username(rabbitMqOptions.Username);
					h.Password(rabbitMqOptions.Password);
				});

				cfg.UseMessageRetry(r => r.Intervals(5, 5));
				cfg.ConfigureEndpoints(ctx);
			});
		});
		return services;
	}

	/// <summary>
	///   This method adds the consumers to the bus configurator.
	/// </summary>
	/// <param name="busConfigurator">The bus configurator.</param>
	private static void AddConsumers(this IRegistrationConfigurator busConfigurator)
	{
		busConfigurator.AddConsumersFromNamespaceContaining<INotificationApplicationMarker>();
		busConfigurator.AddConsumersFromNamespaceContaining<IIdentityApplicationMarker>();
	}

	/// <summary>
	///   This method adds the outbox to the bus configurator.
	/// </summary>
	/// <param name="busConfigurator">The bus configurator.</param>
	private static void AddOutbox(this IBusRegistrationConfigurator busConfigurator) =>
		busConfigurator.AddEntityFrameworkOutbox<JobsRepositoryContext>(options =>
		{
			options.QueryDelay = TimeSpan.FromSeconds(10);
			options.UsePostgres().UseBusOutbox();
		});
}