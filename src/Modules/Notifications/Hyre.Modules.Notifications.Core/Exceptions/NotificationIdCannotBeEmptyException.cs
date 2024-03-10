// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Notifications.Core.Exceptions;

/// <summary>
///   Exception thrown when the identifier of the notification is empty.
/// </summary>
public sealed class NotificationIdCannotBeEmptyException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationIdCannotBeEmptyException" /> class.
	/// </summary>
	public NotificationIdCannotBeEmptyException() : base(NotificationErrorMessages.NotificationIdCannotBeEmpty)
	{
	}
}