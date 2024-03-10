// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;

#endregion

namespace Hyre.Modules.Notifications.Core.Constants;

/// <summary>
///   Error messages for the <see cref="Notification" /> entity.
/// </summary>
internal abstract class NotificationErrorMessages
{
	#region Id

	/// <summary>
	///   Used when the notification identifier is empty.
	/// </summary>
	public const string NotificationIdCannotBeEmpty = "O Id da notificação não pode ser vazio.";

	#endregion
}