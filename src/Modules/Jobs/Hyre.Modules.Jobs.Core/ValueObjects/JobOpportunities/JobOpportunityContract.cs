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
///   Represents the contract of a job opportunity.
/// </summary>
public sealed record JobOpportunityContract : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityContract" /> class.
	/// </summary>
	/// <param name="type">The type of the contract.</param>
	/// <param name="minSalary">The minimum salary of the contract.</param>
	/// <param name="maxSalary">The maximum salary of the contract.</param>
	public JobOpportunityContract(
		ContractType type,
		decimal minSalary,
		decimal maxSalary)
	{
		Type = type;
		MinSalary = minSalary;
		MaxSalary = maxSalary;
		Validate();
	}

	/// <summary>
	///   Gets or sets the type of the contract.
	/// </summary>
	public ContractType Type { get; private set; }

	/// <summary>
	///   Gets or sets the minimum salary of the contract.
	/// </summary>
	public decimal MinSalary { get; }

	/// <summary>
	///   Gets or sets the maximum salary of the contract.
	/// </summary>
	public decimal MaxSalary { get; }

	/// <summary>
	///   This is used to validate the contract of the job opportunity.
	/// </summary>
	/// <exception cref="JobOpportunityMaxSalaryInvalidException">Exception thrown when the minimum salary is invalid.</exception>
	/// <exception cref="JobOpportunityMinSalaryGreaterThanMaxSalaryException">
	///   Exception thrown when the maximum salary is
	///   invalid.
	/// </exception>
	/// <exception cref="JobOpportunityMinSalaryInvalidException">
	///   Exception thrown when the minimum salary is greater than the maximum salary.
	/// </exception>
	private void Validate()
	{
		if (MinSalary < 0)
		{
			throw new JobOpportunityMinSalaryInvalidException();
		}

		if (MaxSalary < 0)
		{
			throw new JobOpportunityMaxSalaryInvalidException();
		}

		if (MinSalary > MaxSalary)
		{
			throw new JobOpportunityMinSalaryGreaterThanMaxSalaryException();
		}
	}
}