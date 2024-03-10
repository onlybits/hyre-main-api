// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Notifications.Core.Exceptions;

/// <summary>
///   Exception thrown when the email is invalid.
/// </summary>
public sealed class EmailInvalidException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="EmailInvalidException" /> class.
	/// </summary>
	/// <param name="email">The email that is invalid.</param>
	public EmailInvalidException(string email) : base(NotificationErrorMessages.EmailInvalid(email))
	{
	}
}