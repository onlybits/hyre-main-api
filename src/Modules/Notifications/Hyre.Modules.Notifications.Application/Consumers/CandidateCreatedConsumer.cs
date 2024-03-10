// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Notifications.Application.Extensions;
using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Modules.Notifications.Core.Repositories;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;

#endregion

namespace Hyre.Modules.Notifications.Application.Consumers;

/// <summary>
///   This consumer is responsible for handling the event of a candidate being created.
/// </summary>
public sealed class CandidateCreatedConsumer : IConsumer<CandidateCreatedEvent>
{
	private readonly ILoggerManager _logger;
	private readonly INotificationsRepositoryManager _repository;

	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateCreatedConsumer" /> class.
	/// </summary>
	/// <param name="repository">The notifications repository manager.</param>
	/// <param name="logger">The logger manager.</param>
	public CandidateCreatedConsumer(INotificationsRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task Consume(ConsumeContext<CandidateCreatedEvent> context)
	{
		_logger.LogInfo("Consuming message for candidate with email: {Email}", context.Message.Email);

		var notificationRecipient = context.Message.ToNotificationRecipient();
		var notification = Notification.Create(notificationRecipient);

		_repository.Notifications.Create(notification);
		await _repository.CommitChangesAsync();
	}
}