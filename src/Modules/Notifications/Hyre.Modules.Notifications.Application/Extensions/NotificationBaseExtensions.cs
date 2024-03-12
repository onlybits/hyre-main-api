// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Notifications.Core.Enums;
using Hyre.Modules.Notifications.Core.ValueObjects;

#endregion

namespace Hyre.Modules.Notifications.Application.Extensions;

/// <summary>
///   This class contains the extensions for the notification.
/// </summary>
public static class NotificationBaseExtensions
{
	/// <summary>
	///   This method converts the candidate created event to a notification recipient.
	/// </summary>
	/// <param name="event">The candidate created event.</param>
	/// <returns>Returns the notification recipient.</returns>
	public static NotificationRecipient ToNotificationRecipient(this CandidateCreatedEvent @event) => new(
		NotificationType.Email,
		@event.Email);
}