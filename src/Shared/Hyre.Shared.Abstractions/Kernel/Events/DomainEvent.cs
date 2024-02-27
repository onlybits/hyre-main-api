// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Kernel.Events;

/// <summary>
///   This is the base class for all domain events.
/// </summary>
public abstract record DomainEvent
{
	/// <summary>
	///   Initializes a new instance of the <see cref="DomainEvent" /> class.
	/// </summary>
	protected DomainEvent() => OccurredAt = DateTime.UtcNow;

	/// <summary>
	///   Gets the date and time when the event occurred.
	/// </summary>
	private DateTime OccurredAt { get; }
}