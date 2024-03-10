// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Notifications.Core.Entities;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Core.Entities;

/// <summary>
///   Unit tests for the <see cref="Notification" /> entity.
/// </summary>
public sealed class NotificationTests
{
	[Fact(DisplayName = nameof(Create_WhenCalled_ShouldCreateNotification))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WhenCalled_ShouldCreateNotification()
	{
		// Arrange
		// Act
		var notification = Notification.Create();

		// Assert
		_ = notification.Should().NotBeNull();
		_ = notification.Id.Should().NotBeNull();
		_ = notification.Id.Value.Should().NotBeEmpty();
	}
}