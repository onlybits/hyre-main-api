// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.ValueObjects;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Notifications.Core.Entities;

/// <summary>
///   Represents a notification sent to a user.
/// </summary>
public sealed class Notification : EntityBase<NotificationId>
{
	/// <summary>
	///   This constructor is used by Entity Framework Core.
	/// </summary>
	private Notification() : base(NotificationId.New())
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="Notification" /> class.
	/// </summary>
	private Notification(NotificationRecipient recipient) : base(NotificationId.New()) => Recipient = recipient;

	/// <summary>
	///   Gets or sets the recipient of the notification.
	/// </summary>
	public NotificationRecipient Recipient { get; private set; }

	/// <summary>
	///   Creates a new instance of the <see cref="Notification" /> class.
	/// </summary>
	/// <param name="recipient">The recipient of the notification.</param>
	/// <returns>Returns a new instance of the <see cref="Notification" /> class.</returns>
	public static Notification Create(NotificationRecipient recipient)
	{
		var notification = new Notification(recipient);
		return notification;
	}
}