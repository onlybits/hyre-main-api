// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Modules.Notifications.Core.Repositories;
using Hyre.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Notifications.Infrastructure.Persistence;

/// <summary>
///   This is the repository contract for the notification entity.
/// </summary>
/// <inheritdoc cref="INotificationRepository" />
internal sealed class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationRepository" /> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public NotificationRepository(DbContext context) : base(context)
	{
	}

	/// <summary>
	///   This method is responsible for creating a new notification.
	/// </summary>
	/// <param name="notification">The notification to be created.</param>
	public void Create(Notification notification) => Insert(notification);
}