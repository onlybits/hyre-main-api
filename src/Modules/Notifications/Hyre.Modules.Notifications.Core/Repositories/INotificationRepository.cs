// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Shared.Abstractions.Repositories;

#endregion

namespace Hyre.Modules.Notifications.Core.Repositories;

/// <summary>
///   This is the repository contract for the notification entity.
/// </summary>
public interface INotificationRepository : IRepositoryBase<NotificationBase>
{
	/// <summary>
	///   This method is responsible for creating a new notification.
	/// </summary>
	/// <param name="notificationBase">The notification to be created.</param>
	void Create(NotificationBase notificationBase);
}