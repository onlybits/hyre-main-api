// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.Events;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Shared.Abstractions.Kernel.Entities;

/// <summary>
///   This class is the base class for all entities in the system.
/// </summary>
/// <inheritdoc cref="IEntityBase" />
public abstract class EntityBase<TId> : IEntityBase where TId : StronglyTypedId<Guid>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="EntityBase{TId}" /> class.
	/// </summary>
	/// <param name="id">The identifier of the entity.</param>
	protected EntityBase(TId id)
	{
		Id = id;
		CreatedAt = CreateDate.Create(TimeProvider.System);
	}

	/// <summary>
	///   Gets the identifier of the entity.
	/// </summary>
	public TId Id { get; private set; }

	/// <summary>
	///   Gets or sets the date and time the entity was created.
	/// </summary>
	public CreateDate CreatedAt { get; private set; }

	#region Events

	/// <summary>
	///   The domain events that occurred in the entity.
	/// </summary>
	private readonly List<DomainEvent> _events = [];

	/// <summary>
	///   Gets the domain events that occurred in the entity.
	/// </summary>
	public ICollection<DomainEvent> Events => _events.AsReadOnly();

	/// <summary>
	///   This method adds a domain event to the entity.
	/// </summary>
	/// <param name="domainEvent"></param>
	public void RaiseEvent(DomainEvent domainEvent) => _events.Add(domainEvent);

	/// <summary>
	///   This method clears the domain events.
	/// </summary>
	public void ClearEvents() => _events.Clear();

	#endregion
}