// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Represents the location of a job opportunity.
/// </summary>
public sealed record JobOpportunityLocation : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityLocation" /> class.
	/// </summary>
	/// <param name="type">The type of the location of the job opportunity.</param>
	/// <param name="city">The city of the job opportunity.</param>
	/// <param name="state">The state of the job opportunity.</param>
	public JobOpportunityLocation(LocationType type, string? city, string? state)
	{
		Type = type;
		City = type is LocationType.Remote ? null : city;
		State = type is LocationType.Remote ? null : state;
		Validate();
	}

	/// <summary>
	///   Gets the type of the location of the job opportunity.
	/// </summary>
	public LocationType Type { get; }

	/// <summary>
	///   Gets the city of the job opportunity.
	/// </summary>
	public string? City { get; }

	/// <summary>
	///   Gets the state of the job opportunity.
	/// </summary>
	public string? State { get; }

	/// <summary>
	///   This is used to validate the location of the job opportunity.
	/// </summary>
	/// <exception cref="JobOpportunityLocationCityTooLongException">
	///   Exception thrown when the location city name of a job opportunity
	///   is too short.
	/// </exception>
	/// <exception cref="JobOpportunityLocationStateTooShortException">
	///   Exception thrown when the location city name of a job opportunity
	///   is too long.
	/// </exception>
	/// <exception cref="JobOpportunityLocationStateTooLongException">
	///   Exception thrown when the location state name of a job
	///   opportunity is too short.
	/// </exception>
	/// <exception cref="JobOpportunityLocationCityTooShortException">
	///   Exception thrown when the location state name of a job
	///   opportunity is too long.
	/// </exception>
	private void Validate()
	{
		if (Type is LocationType.Hybrid or LocationType.OnSite && (City is null || State is null))
		{
			throw new JobOpportunityLocationCantBeNullOnSiteOrHybridException();
		}

		switch (City?.Length)
		{
			case < 3:
				throw new JobOpportunityLocationCityTooShortException();
			case > 32:
				throw new JobOpportunityLocationCityTooLongException();
		}

		switch (State?.Length)
		{
			case < 2:
				throw new JobOpportunityLocationStateTooShortException();
			case > 2:
				throw new JobOpportunityLocationStateTooLongException();
		}
	}
}