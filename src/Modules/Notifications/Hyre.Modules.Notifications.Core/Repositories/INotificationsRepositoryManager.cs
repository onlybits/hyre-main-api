// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Repositories;

#endregion

namespace Hyre.Modules.Notifications.Core.Repositories;

/// <summary>
///   This is the notifications repository manager contract.
/// </summary>
public interface INotificationsRepositoryManager : IRepositoryManager
{
	/// <summary>
	///   Manage the notification repository.
	/// </summary>
	INotificationRepository Notifications { get; }
}