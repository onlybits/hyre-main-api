// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Notifications.Core.Exceptions;
using Hyre.Modules.Notifications.Core.ValueObjects;
using Hyre.Modules.Notifications.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Core.ValueObjects;

/// <summary>
///   Unit tests for <see cref="NotificationId" />.
/// </summary>
public sealed class NotificationIdTests : NotificationFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidValue_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidValue_ShouldCreateInstance()
	{
		// Arrange
		var value = Faker.Random.Guid();

		// Act
		var notificationId = new NotificationId(value);

		// Assert
		_ = notificationId.Should().NotBeNull();
		_ = notificationId.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(Constructor_WithEmptyValue_ShouldThrowNotificationIdCannotBeEmptyException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithEmptyValue_ShouldThrowNotificationIdCannotBeEmptyException()
	{
		// Arrange
		var value = Guid.Empty;

		// Act
		var act = () => new NotificationId(value);

		// Assert
		_ = act.Should()
			.ThrowExactly<NotificationIdCannotBeEmptyException>();
	}

	[Fact(DisplayName = nameof(New_WhenCalled_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void New_WhenCalled_ShouldCreateInstance()
	{
		// Arrange
		// Act
		var notificationId = NotificationId.New();

		// Assert
		_ = notificationId.Should().NotBeNull();
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithGuid_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithGuid_ShouldCreateInstance()
	{
		// Arrange
		var value = Faker.Random.Guid();

		// Act
		NotificationId notificationId = value;

		// Assert
		_ = notificationId.Should().NotBeNull();
		_ = notificationId.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithNotificationId_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithNotificationId_ShouldCreateInstance()
	{
		// Arrange
		var value = Faker.Random.Guid();
		var notificationId = new NotificationId(value);

		// Act
		Guid guid = notificationId;

		// Assert
		_ = guid.Should().Be(value);
	}
}