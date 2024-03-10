// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Shared.Abstractions.Exceptions;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Represents the name of a job opportunity.
/// </summary>
public sealed record JobOpportunityName : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityName" /> class.
	/// </summary>
	/// <param name="value">The value of the job opportunity name.</param>
	public JobOpportunityName(string value)
	{
		Value = value;
		Validate();
	}

	/// <summary>
	///   Gets the value of the job opportunity name.
	/// </summary>
	public string Value { get; }

	/// <summary>
	///   This is used to validate the name of the job opportunity.
	/// </summary>
	/// <exception cref="DomainException">Exception thrown when the name has an invalid state.</exception>
	private void Validate()
	{
		switch (Value.Length)
		{
			case < 3:
				throw new JobOpportunityNameTooShortException();
			case > 64:
				throw new JobOpportunityNameTooLongException();
		}
	}

	#region Implicit Operators

	/// <summary>
	///   Implicitly converts the <see cref="JobOpportunityName" /> to a <see cref="string" />.
	/// </summary>
	/// <param name="name">The name of the job opportunity.</param>
	/// <returns>The value of the job opportunity name.</returns>
	public static implicit operator string(JobOpportunityName name) => name.Value;

	/// <summary>
	///   Implicitly converts the <see cref="string" /> to a <see cref="JobOpportunityName" />.
	/// </summary>
	/// <param name="value">The value of the job opportunity name.</param>
	/// <returns>The name of the job opportunity.</returns>
	public static implicit operator JobOpportunityName(string value) => new(value);

	#endregion
}