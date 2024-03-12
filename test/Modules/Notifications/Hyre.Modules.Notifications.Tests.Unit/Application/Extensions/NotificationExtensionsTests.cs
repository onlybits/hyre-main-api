// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Notifications.Application.Extensions;
using Hyre.Modules.Notifications.Core.Enums;
using Hyre.Modules.Notifications.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Application.Extensions;

/// <summary>
///   Unit tests for the notification extensions.
/// </summary>
public sealed class NotificationExtensionsTests : NotificationFixture
{
	[Fact(DisplayName = nameof(ToNotificationRecipient_WhenUsedInAnEvent_ShouldMapToNotificationRecipientObject))]
	[Trait(ExtensionsTraits.Name, ExtensionsTraits.Value)]
	public void ToNotificationRecipient_WhenUsedInAnEvent_ShouldMapToNotificationRecipientObject()
	{
		// Arrange
		var address = Faker.Internet.Email();
		var notification = new CandidateCreatedEvent(address);

		// Act
		var recipient = notification.ToNotificationRecipient();

		// Assert
		_ = recipient.Should().NotBeNull();
		_ = recipient.Type.Should().Be(NotificationType.Email);
		_ = recipient.Address.Should().Be(address);
	}
}