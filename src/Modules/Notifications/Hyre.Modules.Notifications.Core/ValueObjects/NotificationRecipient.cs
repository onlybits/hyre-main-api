using System.ComponentModel.DataAnnotations;
using Hyre.Modules.Notifications.Core.Enums;
using Hyre.Modules.Notifications.Core.Exceptions;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

namespace Hyre.Modules.Notifications.Core.ValueObjects;

/// <summary>
///   Represents a recipient of a notification.
/// </summary>
public sealed record NotificationRecipient : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="NotificationRecipient" /> class.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="address"></param>
	public NotificationRecipient(
		NotificationType type,
		string address)
	{
		Type = type;
		Address = address;
		Validate();
	}

	/// <summary>
	///   Gets the type of the recipient.
	/// </summary>
	public NotificationType Type { get; }

	/// <summary>
	///   Gets the address of the recipient.
	/// </summary>
	public string Address { get; }

	/// <summary>
	///   This method is used to validate the recipient of the notification.
	/// </summary>
	/// <exception cref="EmailInvalidException">Exception thrown when the email address is invalid.</exception>
	private void Validate()
	{
		if (Type is NotificationType.Email && !new EmailAddressAttribute().IsValid(Address))
		{
			throw new EmailInvalidException(Address);
		}
	}
}