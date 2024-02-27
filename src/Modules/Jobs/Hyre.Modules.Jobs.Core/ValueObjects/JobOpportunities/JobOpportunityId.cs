// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Represents the unique identifier of a job opportunity.
/// </summary>
public sealed record JobOpportunityId : StronglyTypedId<Guid>
{
	/// <summary>
	///   Represents the unique identifier of a job opportunity.
	/// </summary>
	/// <param name="Value">The value of the identifier.</param>
	public JobOpportunityId(Guid Value) : base(Value) => Validate();

	/// <summary>
	///   Creates a new instance of the <see cref="JobOpportunityId" /> class.
	/// </summary>
	/// <returns>It will return a new instance of the <see cref="JobOpportunityId" /> class.</returns>
	public static JobOpportunityId New() => new(Guid.NewGuid());

	/// <summary>
	///   This is used to validate the identifier.
	/// </summary>
	/// <exception cref="DomainException">Exception thrown when the identifier has an invalid state.</exception>
	private void Validate()
	{
		if (Value == Guid.Empty)
		{
			throw new DomainException(JobOpportunityErrorMessages.IdCannotBeEmpty);
		}
	}
}