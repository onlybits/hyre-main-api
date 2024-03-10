// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Repositories;
using Hyre.Modules.Notifications.Infrastructure.Persistence;

#endregion

namespace Hyre.Modules.Notifications.Infrastructure;

/// <summary>
///   This class is responsible for managing the all the repositories for the notifications module.
/// </summary>
internal sealed class NotificationsRepositoryManager : INotificationsRepositoryManager
{
	private readonly NotificationsRepositoryContext _context;
	private readonly Lazy<INotificationRepository> _notificationsRepository;

	public NotificationsRepositoryManager(NotificationsRepositoryContext context)
	{
		_context = context;
		_notificationsRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(_context));
	}

	public INotificationRepository Notifications => _notificationsRepository.Value;

	public async Task CommitChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
}