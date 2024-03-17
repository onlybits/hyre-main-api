// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Jobs.Core.Constants;

/// <summary>
///   Error messages for the candidate entity properties.
/// </summary>
public abstract class CandidateErrorMessages
{
	#region Id

	/// <summary>
	///   Used when the identifier of the candidate is empty.
	/// </summary>
	public const string IdCannotBeEmpty = "O identificador do candidato não pode ser vazio.";

	#endregion

	#region Document

	/// <summary>
	///   Used when the document of the candidate is not valid.
	/// </summary>
	public const string DocumentNotValid = "O documento informado não é válido.";

	#endregion

	#region Date of birth

	/// <summary>
	///   Used when the date of birth of the candidate is not valid.
	/// </summary>
	public const string DateOfBirthNotValid = "O candidato deve ter no mínimo 16 anos.";

	#endregion

	#region Summary

	/// <summary>
	///   Used when the summary of the candidate is not valid.
	/// </summary>
	public const string SummaryValueNotValid = "O resumo deve conter entre 50 e 1000 caracteres.";

	#endregion

	#region Email

	/// <summary>
	///   Used when the email of the candidate is invalid.
	/// </summary>
	/// <param name="email">The email of the candidate.</param>
	public static string EmailInvalid(string email) => $"O e-mail: '{email}' é inválido.";

	#endregion

	#region Address

	/// <summary>
	///   Used when the zip code address of the candidate is not valid.
	/// </summary>
	public const string ZipCodeNotValid = "O CEP informado não é válido.";

	/// <summary>
	///   Used when the country address of the candidate is not valid.
	/// </summary>
	public const string AddressCountryNotValid = "O país deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Used when the state address of the candidate is not valid.
	/// </summary>
	public const string AddressStateNotValid = "O estado deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Used when the city address of the candidate is not valid.
	/// </summary>
	public const string AddressCityNotValid = "A cidade deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Used when the neighborhood address of the candidate is not valid.
	/// </summary>
	public const string AddressComplementNotValid = "O complemento do endereço deve conter entre 3 e 100 caracteres.";

	/// <summary>
	///   Used when the neighborhood address of the candidate is not valid.
	/// </summary>
	public const string AddressNumberNotValid = "O número do endereço é inválido.";

	/// <summary>
	///   Used when the neighborhood address of the candidate is not valid.
	/// </summary>
	public const string AddressNeighborhoodNotValid = "O bairro deve conter entre 3 e 50 caracteres.";

	#endregion

	#region Phone Number

	/// <summary>
	///   Used when the phone number of the candidate is not valid.
	/// </summary>
	public const string PhoneNumberValueNotValid = "O número de telefone informado não é válido.";

	/// <summary>
	///   Used when the area code of the phone number is not valid.
	/// </summary>
	public const string PhoneNumberAreaCodeNotValid = "O DDD informado não é válido.";

	#endregion

	#region Application

	/// <summary>
	///   Used when the candidate is not found.
	/// </summary>
	public const string NotFound = "O candidato não foi encontrado.";

	/// <summary>
	///   Used when the candidate already exists by email.
	/// </summary>
	public const string AlreadyExistsByEmail = "Já existe um candidato com o e-mail informado cadastrado para esta oportunidade.";

	#endregion

	#region Name

	/// <summary>
	///   Used when the first name of the candidate is too short.
	/// </summary>
	public const string FirstNameTooShort = "O primeiro nome do candidato deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the middle name of the candidate is too short.
	/// </summary>
	public const string MiddleNameTooShort = "O nome do meio do candidato deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the last name of the candidate is too short.
	/// </summary>
	public const string LastNameTooShort = "O último nome do candidato deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the first name of the candidate is too long.
	/// </summary>
	public const string FirstNameTooLong = "O primeiro nome do candidato deve ter no máximo 32 caracteres.";

	/// <summary>
	///   Used when the middle name of the candidate is too long.
	/// </summary>
	public const string MiddleNameTooLong = "O nome do meio do candidato deve ter no máximo 32 caracteres.";

	/// <summary>
	///   Used when the last name of the candidate is too long.
	/// </summary>
	public const string LastNameTooLong = "O último nome do candidato deve ter no máximo 32 caracteres.";

	#endregion

	#region Education

	/// <summary>
	///   Used when the course of the candidate's education is not valid.
	/// </summary>
	public const string EducationStartDateInvalid = "A data de início da educação é inválida.";

	/// <summary>
	///   Used when the course of the candidate's education is not valid.
	/// </summary>
	public const string EducationEndDateInvalid = "A data de término da educação é inválida.";

	/// <summary>
	///   Used when the course of the candidate's education is not valid.
	/// </summary>
	public const string EducationInstitutionInvalid = "A instituição deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Used when the course of the candidate's education is not valid.
	/// </summary>
	public const string EducationCourseInvalid = "O curso deve conter entre 3 e 50 caracteres.";

	#endregion

	#region Experience

	/// <summary>
	///   Used when the start date of the candidate's experience is not valid.
	/// </summary>
	public const string ExperienceStartDateInvalid = "A data de início da experiência é inválida.";

	/// <summary>
	///   Used when the end date of the candidate's experience is not valid.
	/// </summary>
	public const string ExperienceEndDateInvalid = "A data de término da experiência é inválida.";

	/// <summary>
	///   Used when the position of the candidate's experience is not valid.
	/// </summary>
	public const string ExperiencePositionInvalid = "A posição deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Exception thrown when the company of the candidate's experience is not valid.
	/// </summary>
	public const string ExperienceCompanyInvalid = "A empresa deve conter entre 3 e 50 caracteres.";

	/// <summary>
	///   Used when the description of the candidate's experience is not valid.
	/// </summary>
	public const string ExperienceDescriptionInvalid = "A descrição da experiência deve conter entre 3 e 50 caracteres.";

	#endregion

	#region Social Network

	/// <summary>
	///   Exception thrown when the GitHub of the candidate's social network is not valid.
	/// </summary>
	public const string SocialNetworkGitHubInvalid = "O GitHub deve conter entre 3 e 32 caracteres.";

	/// <summary>
	///   Used when the LinkedIn of the candidate's social network is not valid.
	/// </summary>
	public const string SocialNetworkLinkedinInvalid = "O LinkedIn deve conter entre 3 e 32 caracteres.";

	#endregion
}