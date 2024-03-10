// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Notifications.Core.Enums;
using Hyre.Modules.Notifications.Core.ValueObjects;
using Hyre.Modules.Notifications.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Core.ValueObjects;

/// <summary>
///   Unit tests for <see cref="NotificationRecipient" />.
/// </summary>
public sealed class NotificationRecipientTests : NotificationFixture
{
	[Fact(DisplayName = nameof(Create_WhenCalled_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Create_WhenCalled_ShouldCreateInstance()
	{
		// Arrange
		var type = Faker.Random.Enum<NotificationType>();
		var address = string.Empty;
		if (type is NotificationType.Email)
		{
			address = Faker.Internet.Email();
		}

		// Act
		var notificationRecipient = new NotificationRecipient(type, address);

		// Assert
		_ = notificationRecipient.Should().NotBeNull();
		_ = notificationRecipient.Type.Should().Be(type);
		_ = notificationRecipient.Address.Should().Be(address);
	}
}