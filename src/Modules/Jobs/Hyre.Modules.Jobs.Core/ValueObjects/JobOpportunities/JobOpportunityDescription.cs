// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Represents the description of a job opportunity.
/// </summary>
public sealed record JobOpportunityDescription : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityDescription" /> class.
	/// </summary>
	/// <param name="description">The description of the job opportunity.</param>
	public JobOpportunityDescription(string description)
	{
		Value = description;
		Validate();
	}

	/// <summary>
	///   Gets the description of the job opportunity.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   This method is used to validate the description of the job opportunity.
	/// </summary>
	/// <exception cref="JobOpportunityDescriptionTooLongException">Exception thrown when the description is too short.</exception>
	/// <exception cref="JobOpportunityDescriptionTooShortException">Exception thrown when the description is too long.</exception>
	private void Validate()
	{
		switch (Value.Length)
		{
			case < 10:
				throw new JobOpportunityDescriptionTooShortException();
			case > 500:
				throw new JobOpportunityDescriptionTooLongException();
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts the <see cref="JobOpportunityDescription" /> to a <see cref="string" />.
	/// </summary>
	/// <param name="description">The description of the job opportunity.</param>
	/// <returns>It will return the value of the job opportunity description.</returns>
	public static implicit operator string(JobOpportunityDescription description) => description.Value;

	/// <summary>
	///   Implicitly converts the <see cref="string" /> to a <see cref="JobOpportunityDescription" />.
	/// </summary>
	/// <param name="description">The value of the description.</param>
	/// <returns>It will return a new instance of the <see cref="JobOpportunityDescription" /> class.</returns>
	public static implicit operator JobOpportunityDescription(string description) => new(description);

	#endregion
}