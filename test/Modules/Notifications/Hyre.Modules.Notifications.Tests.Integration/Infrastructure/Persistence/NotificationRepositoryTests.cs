// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Notifications.Infrastructure;
using Hyre.Modules.Notifications.Infrastructure.Persistence;
using Hyre.Modules.Notifications.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Notifications.Tests.Integration.Infrastructure.Persistence;

/// <summary>
///   Integration tests for the notification repository.
/// </summary>
public sealed class NotificationRepositoryTests : NotificationBaseFixture, IAsyncLifetime
{
	private readonly NotificationsRepositoryContext _context;
	private readonly NotificationRepository _sut;

	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationRepositoryTests" /> class.
	/// </summary>
	/// <param name="factory">The web application factory.</param>
	public NotificationRepositoryTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
		_context = CreateRepositoryContext();
		_sut = new NotificationRepository(_context);
	}

	/// <summary>
	///   Runs before the test execution.
	/// </summary>
	public async Task InitializeAsync() => await Task.CompletedTask;

	/// <summary>
	///   Runs after the test execution.
	/// </summary>
	public async Task DisposeAsync() => await _context.Notifications.ExecuteDeleteAsync();

	[Fact(DisplayName = nameof(Create_WhenGivenNotification_ShouldCreateNotification))]
	[Trait(PersistenceTraits.Name, PersistenceTraits.Value)]
	public async Task Create_WhenGivenNotification_ShouldCreateNotification()
	{
		// Arrange
		var notification = GenerateNotification();

		// Act
		_sut.Create(notification);
		_ = await _context.SaveChangesAsync();

		var result = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notification.Id);

		// Assert
		_ = result.Should().NotBeNull();
		_ = result!.Id.Should().Be(notification.Id);
		_ = result.Recipient.Should().Be(notification.Recipient);
	}
}