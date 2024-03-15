// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Core.Entities;
using Hyre.Modules.Notifications.Core.Exceptions;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Notifications.Core.ValueObjects;

/// <summary>
///   Represents the unique identifier of a <see cref="NotificationBase" />.
/// </summary>
public sealed record NotificationBaseId : StronglyTypedId<Guid>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationBaseId" /> class.
	/// </summary>
	/// <param name="Value">The value of the identifier.</param>
	public NotificationBaseId(Guid Value) : base(Value) => Validate();

	/// <summary>
	///   Creates a new instance of the <see cref="NotificationBaseId" /> class.
	/// </summary>
	/// <returns>Returns a new instance of the <see cref="NotificationBaseId" /> class.</returns>
	public static NotificationBaseId New() => new(Guid.NewGuid());

	/// <summary>
	///   This method is used to validate the identifier of the notification.
	/// </summary>
	private void Validate()
	{
		if (Value == Guid.Empty)
		{
			throw new NotificationIdCannotBeEmptyException();
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts the <see cref="Guid" /> to a <see cref="NotificationBaseId" />.
	/// </summary>
	/// <param name="value">The value to be converted.</param>
	/// <returns>Returns the <see cref="NotificationBaseId" />.</returns>
	public static implicit operator NotificationBaseId(Guid value) => new(value);

	/// <summary>
	///   Implicitly converts the <see cref="NotificationBaseId" /> to a <see cref="Guid" />.
	/// </summary>
	/// <param name="value">The value to be converted.</param>
	/// <returns>Returns the <see cref="Guid" />.</returns>
	public static implicit operator Guid(NotificationBaseId value) => value.Value;

	#endregion
}