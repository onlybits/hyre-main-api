// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Application.Common;

/// <summary>
///   Represents the response of a candidate.
/// </summary>
/// <param name="Id">The candidate identifier.</param>
/// <param name="Name">The candidate's name.</param>
/// <param name="Email">The candidate's email.</param>
/// <param name="Document">The candidate's document.</param>
/// <param name="DateOfBirth">The candidate's date of birth.</param>
/// <param name="Seniority">The candidate's experience level.</param>
/// <param name="Disability">The candidate's disability.</param>
/// <param name="Gender">The candidate's gender.</param>
/// <param name="PhoneNumber">The candidate's phone number.</param>
/// <param name="Address">The candidate's address.</param>
/// <param name="Educations">The candidate's educations.</param>
/// <param name="Experiences">The candidate's experiences.</param>
/// <param name="SocialNetwork">The candidate's social network.</param>
/// <param name="Languages">The candidate's languages.</param>
/// <param name="CreatedAt">The candidate creation date.</param>
public sealed record CandidateResponse(
	CandidateId Id,
	CandidateName Name,
	CandidateEmail Email,
	CandidateDocument Document,
	CandidateDateOfBirth DateOfBirth,
	CandidateSeniority Seniority,
	CandidateDisability? Disability,
	CandidateGender Gender,
	CandidatePhoneNumber PhoneNumber,
	CandidateAddress Address,
	ICollection<CandidateEducation>? Educations,
	ICollection<CandidateExperience>? Experiences,
	CandidateSocialNetwork? SocialNetwork,
	ICollection<CandidateLanguage>? Languages,
	CreateDate CreatedAt);