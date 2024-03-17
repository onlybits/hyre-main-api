// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the work experience.
/// </summary>
public sealed record CandidateExperience : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateExperience" /> class.
	/// </summary>
	/// <param name="startDate">The start date of the experience.</param>
	/// <param name="endDate">The end date of the experience.</param>
	/// <param name="position">The position of the experience.</param>
	/// <param name="company">The company of the experience.</param>
	/// <param name="description">The description of the experience.</param>
	public CandidateExperience(
		DateOnly startDate,
		DateOnly? endDate,
		string position,
		string company,
		string description)
	{
		StartDate = startDate;
		EndDate = endDate;
		Position = position;
		Company = company;
		Description = description;
		Validate();
	}

	/// <summary>
	///   Gets the start date of the experience.
	/// </summary>
	public DateOnly StartDate { get; }

	/// <summary>
	///   Gets the end date of the experience.
	/// </summary>
	public DateOnly? EndDate { get; }

	/// <summary>
	///   Gets the title of the experience.
	/// </summary>
	public string Position { get; }

	/// <summary>
	///   Gets the company of the experience.
	/// </summary>
	public string Company { get; }

	/// <summary>
	///   Gets the description of the experience.
	/// </summary>
	public string Description { get; }

	/// <summary>
	///   This method is used to validate the object.
	/// </summary>
	private void Validate()
	{
		if (StartDate >= DateOnly.FromDateTime(DateTime.Now))
		{
			throw new CandidateExperienceStartDateInvalidException();
		}


		if (EndDate <= StartDate)
		{
			throw new CandidateExperienceEndDateInvalidException();
		}

		if (Position.Length is < 3 or > 50)
		{
			throw new CandidateExperiencePositionInvalidException();
		}

		if (Company.Length is < 3 or > 50)
		{
			throw new CandidateExperienceCompanyInvalidException();
		}

		if (Description.Length is < 3 or > 500)
		{
			throw new CandidateExperienceDescriptionInvalidException();
		}
	}
}