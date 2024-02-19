// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.Exceptions;

#endregion

namespace Hyre.Shared.Abstractions.Kernel.ValueObjects;

/// <summary>
///   Represents the creation date of an entity.
/// </summary>
public sealed record CreateDate : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CreateDate" /> class.
	/// </summary>
	/// <param name="value">The creation date.</param>
	public CreateDate(DateTimeOffset value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets or sets the creation date.
	/// </summary>
	public DateTimeOffset Value { get; }

	/// <summary>
	///   Creates a new instance of the <see cref="CreateDate" /> class.
	/// </summary>
	/// <param name="timeProvider">The system time provider.</param>
	/// <returns>Returns a new instance of the <see cref="CreateDate" /> class.</returns>
	public static CreateDate Create(TimeProvider timeProvider) => new(timeProvider.GetUtcNow());

	/// <summary>
	///   This method validates the creation date.
	/// </summary>
	/// <exception cref="CreateDateWithEmptyValueException">Exception thrown when the creation date is empty.</exception>
	/// <exception cref="CreateDateInTheFutureException">Exception thrown when the creation date is in the future.</exception>
	private void Validate()
	{
		if (Value == new DateTimeOffset(default))
		{
			throw new CreateDateWithEmptyValueException();
		}

		if (Value > DateTimeOffset.UtcNow)
		{
			throw new CreateDateInTheFutureException();
		}
	}
}