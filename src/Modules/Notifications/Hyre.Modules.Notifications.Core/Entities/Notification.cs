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
	///   Initializes a new instance of the <see cref="Notification" /> class.
	/// </summary>
	private Notification() : base(NotificationId.New())
	{
	}

	/// <summary>
	///   Creates a new instance of the <see cref="Notification" /> class.
	/// </summary>
	/// <returns>Returns a new instance of the <see cref="Notification" /> class.</returns>
	public static Notification Create()
	{
		var notification = new Notification();
		return notification;
	}
}