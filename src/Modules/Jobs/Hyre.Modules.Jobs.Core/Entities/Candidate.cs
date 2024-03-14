// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Jobs.Core.Entities;

/// <summary>
///   Represents a candidate for a job opportunity in the system.
/// </summary>
public sealed class Candidate : EntityBase<CandidateId>
{
	/// <summary>
	///   This constructor is only used by EF Core.
	/// </summary>
	private Candidate() : base(CandidateId.New())
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="id">The candidate's id.</param>
	/// <param name="name">The candidate's name.</param>
	/// <param name="email">The candidate's email.</param>
	/// <param name="document">The candidate's document.</param>
	/// <param name="dateOfBirth">The candidate's date of birth.</param>
	/// <param name="seniority">The candidate's experience level.</param>
	/// <param name="disability">The candidate's disability.</param>
	/// <param name="gender">The candidate's gender.</param>
	/// <param name="phoneNumber">The candidate's phone number.</param>
	/// <param name="address">The candidate's address.</param>
	/// <param name="educations">The candidate's education.</param>
	/// <param name="experiences">The candidate's experience.</param>
	/// <param name="jobOpportunities">The job opportunities where the candidate is applying.</param>
	/// <param name="socialNetwork">The candidate's social network.</param>
	/// <param name="languages">The candidate's languages.</param>
	private Candidate(
		CandidateId id,
		CandidateName name,
		CandidateEmail email,
		CandidateDocument document,
		CandidateDateOfBirth dateOfBirth,
		CandidateSeniority seniority,
		CandidateDisability disability,
		CandidateGender gender,
		CandidatePhoneNumber phoneNumber,
		CandidateAddress address,
		ICollection<CandidateEducation> educations,
		ICollection<CandidateExperience> experiences,
		ICollection<JobOpportunity> jobOpportunities,
		CandidateSocialNetwork socialNetwork,
		ICollection<CandidateLanguage> languages) : base(id)
	{
		Name = name;
		Email = email;
		Document = document;
		DateOfBirth = dateOfBirth;
		Seniority = seniority;
		Disability = disability;
		Gender = gender;
		PhoneNumber = phoneNumber;
		Address = address;
		Educations = educations;
		Experiences = experiences;
		JobOpportunities = jobOpportunities;
		SocialNetwork = socialNetwork;
		Languages = languages;
	}

	/// <summary>
	///   Gets or sets the candidate's name.
	/// </summary>
	public CandidateName Name { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's email.
	/// </summary>
	public CandidateEmail Email { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's document.
	/// </summary>
	public CandidateDocument Document { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's date of birth.
	/// </summary>
	public CandidateDateOfBirth DateOfBirth { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's experience level.
	/// </summary>
	public CandidateSeniority Seniority { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's disability.
	/// </summary>
	public CandidateDisability Disability { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's gender.
	/// </summary>
	public CandidateGender Gender { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's phone number.
	/// </summary>
	public CandidatePhoneNumber PhoneNumber { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's address.
	/// </summary>
	public CandidateAddress Address { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's social network.
	/// </summary>
	public CandidateSocialNetwork SocialNetwork { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's educations.
	/// </summary>
	public ICollection<CandidateEducation> Educations { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's experiences.
	/// </summary>
	public ICollection<CandidateExperience> Experiences { get; private set; }

	/// <summary>
	///   Gets or sets the candidate's languages.
	/// </summary>
	public ICollection<CandidateLanguage> Languages { get; private set; }

	#region EF Core Relationships

	/// <summary>
	///   Gets the job opportunities where the candidate is applying.
	/// </summary>
	public ICollection<JobOpportunity> JobOpportunities { get; private set; }

	#endregion

	/// <summary>
	///   Creates a new instance of the <see cref="Candidate" /> class.
	/// </summary>
	/// <param name="name">The candidate's name.</param>
	/// <param name="email">The candidate's email.</param>
	/// <param name="document">The candidate's document.</param>
	/// <param name="dateOfBirth">The candidate's date of birth.</param>
	/// <param name="seniority">The candidate's experience level.</param>
	/// <param name="disability">The candidate's disability.</param>
	/// <param name="gender">The candidate's gender.</param>
	/// <param name="phoneNumber">The candidate's phone number.</param>
	/// <param name="address">The candidate's address.</param>
	/// <param name="educations">The candidate's educations.</param>
	/// <param name="experiences">The candidate's experiences.</param>
	/// <param name="jobOpportunities">The job opportunities where the candidate is applying.</param>
	/// <param name="socialNetwork">The candidate's social network.</param>
	/// <param name="languages">The candidate's languages.</param>
	/// <returns>Returns a new instance of the <see cref="Candidate" /> class.</returns>
	public static Candidate Create(
		CandidateName name,
		CandidateEmail email,
		CandidateDocument document,
		CandidateDateOfBirth dateOfBirth,
		CandidateSeniority seniority,
		CandidateDisability disability,
		CandidateGender gender,
		CandidatePhoneNumber phoneNumber,
		CandidateAddress address,
		ICollection<CandidateEducation> educations,
		ICollection<CandidateExperience> experiences,
		ICollection<JobOpportunity> jobOpportunities,
		CandidateSocialNetwork socialNetwork,
		ICollection<CandidateLanguage> languages)
	{
		var candidate = new Candidate(
			CandidateId.New(),
			name,
			email,
			document,
			dateOfBirth,
			seniority,
			disability,
			gender,
			phoneNumber,
			address,
			educations,
			experiences,
			jobOpportunities,
			socialNetwork,
			languages);

		var @event = new CandidateCreatedEvent(email.Value, document.Value);
		candidate.RaiseEvent(@event);

		return candidate;
	}

	#region Updates

	/// <summary>
	///   This method updates the candidate's name.
	/// </summary>
	/// <param name="name">The new candidate's name.</param>
	public void UpdateName(CandidateName? name) => Name = name ?? Name;

	#endregion
}