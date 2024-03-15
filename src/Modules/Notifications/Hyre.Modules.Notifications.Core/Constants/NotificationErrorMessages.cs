// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;

#endregion

namespace Hyre.Modules.Notifications.Core.Constants;

/// <summary>
///   Error messages for the <see cref="NotificationBase" /> entity.
/// </summary>
internal abstract class NotificationErrorMessages
{
	#region Id

	/// <summary>
	///   Used when the notification identifier is empty.
	/// </summary>
	public const string NotificationIdCannotBeEmpty = "O Id da notificação não pode ser vazio.";

	#endregion

	#region Email

	/// <summary>
	///   Used when the email is invalid.
	/// </summary>
	/// <param name="email">The email that is invalid.</param>
	public static string EmailInvalid(string email) => $"O e-mail: '{email}' é inválido.";

	#endregion
}