// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Represents the requirements of a job opportunity.
/// </summary>
public sealed record JobOpportunityRequirements : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityRequirements" /> class.
	/// </summary>
	/// <param name="values">The value of the job opportunity requirement.</param>
	public JobOpportunityRequirements(ICollection<string> values)
	{
		Values = values;
		Validate();
	}

	/// <summary>
	///   Gets the value of the job opportunity requirement.
	/// </summary>
	public ICollection<string> Values { get; }

	/// <summary>
	///   This method is used to validate the requirements of the job opportunity.
	/// </summary>
	private void Validate()
	{
		if (Values.Count == 0)
		{
			return;
		}

		foreach (var value in Values.Select((name, index) => (name, index)))
		{
			switch (value.name.Length)
			{
				case < 3:
					throw new JobOpportunityRequirementTooShortException(value.index + 1);
				case > 500:
					throw new JobOpportunityRequirementTooLongException(value.index + 1);
			}
		}
	}
}