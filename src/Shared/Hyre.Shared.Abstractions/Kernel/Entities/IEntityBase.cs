// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.Events;

#endregion

namespace Hyre.Shared.Abstractions.Kernel.Entities;

/// <summary>
///   This interface represents the base of all entities in the system.
/// </summary>
public interface IEntityBase
{
	/// <summary>
	///   Gets the domain events that occurred in the entity.
	/// </summary>
	ICollection<DomainEvent> Events { get; }

	/// <summary>
	///   This method adds a domain event to the entity.
	/// </summary>
	/// <param name="domainEvent">The domain event to be added.</param>
	void RaiseEvent(DomainEvent domainEvent);

	/// <summary>
	///   This method clears the domain events.
	/// </summary>
	void ClearEvents();
}