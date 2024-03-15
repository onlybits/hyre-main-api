// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Modules.Notifications.Core.Enums;
using Hyre.Modules.Notifications.Core.ValueObjects;

#endregion

namespace Hyre.Modules.Notifications.Tests.Integration.Common;

/// <summary>
///   The base class fixture for all notification tests.
/// </summary>
public abstract class NotificationBaseFixture : BaseFixture
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationBaseFixture" /> class.
	/// </summary>
	/// <param name="factory">The integration tests web application factory.</param>
	protected NotificationBaseFixture(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
	}

	/// <summary>
	///   Generates a new <see cref="NotificationBase" />.
	/// </summary>
	/// <returns>Returns a new <see cref="NotificationBase" />.</returns>
	protected NotificationBase GenerateNotification() => NotificationBase.Create(
		GenerateNotificationRecipient());

	/// <summary>
	///   Generates a new <see cref="NotificationRecipient" />.
	/// </summary>
	/// <returns>Returns a new <see cref="NotificationRecipient" />.</returns>
	protected NotificationRecipient GenerateNotificationRecipient()
	{
		var type = Faker.Random.Enum<NotificationType>();
		var address = string.Empty;

		if (type is NotificationType.Email)
		{
			address = Faker.Internet.Email();
		}

		return new NotificationRecipient(type, address);
	}
}