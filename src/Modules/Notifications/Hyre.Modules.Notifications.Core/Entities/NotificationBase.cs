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
public sealed class NotificationBase : EntityBase<NotificationBaseId>
{
	/// <summary>
	///   This constructor is used by Entity Framework Core.
	/// </summary>
	private NotificationBase() : base(NotificationBaseId.New())
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationBase" /> class.
	/// </summary>
	private NotificationBase(NotificationRecipient recipient) : base(NotificationBaseId.New()) => Recipient = recipient;

	/// <summary>
	///   Gets or sets the recipient of the notification.
	/// </summary>
	public NotificationRecipient Recipient { get; private set; }

	/// <summary>
	///   Creates a new instance of the <see cref="NotificationBase" /> class.
	/// </summary>
	/// <param name="recipient">The recipient of the notification.</param>
	/// <returns>Returns a new instance of the <see cref="NotificationBase" /> class.</returns>
	public static NotificationBase Create(NotificationRecipient recipient)
	{
		var notification = new NotificationBase(recipient);
		return notification;
	}
}