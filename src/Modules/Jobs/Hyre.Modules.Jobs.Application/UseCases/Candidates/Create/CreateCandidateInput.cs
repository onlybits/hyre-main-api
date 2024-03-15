// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;

/// <summary>
///   Represents the input data for the create candidate use case.
/// </summary>
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
public sealed record CreateCandidateInput(
	CandidateName Name,
	CandidateEmail Email,
	CandidateDocument Document,
	CandidateDateOfBirth DateOfBirth,
	CandidateSeniority Seniority,
	CandidateDisability Disability,
	CandidateGender Gender,
	CandidatePhoneNumber PhoneNumber,
	CandidateAddress Address,
	ICollection<CandidateEducation> Educations,
	ICollection<CandidateExperience> Experiences,
	CandidateSocialNetwork SocialNetwork,
	ICollection<CandidateLanguage> Languages);