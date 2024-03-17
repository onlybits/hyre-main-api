// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the school degree.
/// </summary>
public sealed record CandidateEducation : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateEducation" /> class.
	/// </summary>
	/// <param name="startDate">The start date of the education.</param>
	/// <param name="endDate">The end date of the education.</param>
	/// <param name="institution">The institution of the education.</param>
	/// <param name="course">The course of the education.</param>
	/// <param name="degree">The degree of the education.</param>
	public CandidateEducation(
		DateOnly startDate,
		DateOnly? endDate,
		string institution,
		string course,
		Degree degree)
	{
		StartDate = startDate;
		EndDate = endDate;
		Institution = institution;
		Course = course;
		Degree = degree;
		Validate();
	}

	/// <summary>
	///   Gets the start date of the education.
	/// </summary>
	public DateOnly StartDate { get; }

	/// <summary>
	///   Gets the end date of the education.
	/// </summary>
	public DateOnly? EndDate { get; }

	/// <summary>
	///   Gets the institution of the education.
	/// </summary>
	public string Institution { get; }

	/// <summary>
	///   Gets the course of the education.
	/// </summary>
	public string Course { get; }

	/// <summary>
	///   Gets the degree of the education.
	/// </summary>
	public Degree Degree { get; }

	/// <summary>
	///   The method to validate the education.
	/// </summary>
	private void Validate()
	{
		if (StartDate >= DateOnly.FromDateTime(DateTime.Now))
		{
			throw new CandidateEducationStartDateInvalidException();
		}

		if (EndDate <= StartDate)
		{
			throw new CandidateEducationEndDateInvalidException();
		}

		if (Institution.Length is < 3 or > 50)
		{
			throw new CandidateEducationInstitutionInvalidException();
		}

		if (Course.Length is < 3 or > 50)
		{
			throw new CandidateEducationCourseInvalidException();
		}
	}
}