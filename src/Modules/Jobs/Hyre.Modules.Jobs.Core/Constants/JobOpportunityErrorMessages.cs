// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Jobs.Core.Constants;

/// <summary>
///   Error messages for the job opportunity entity properties.
/// </summary>
public abstract class JobOpportunityErrorMessages
{
	#region 00 - Application

	/// <summary>
	///   Used when the job opportunity is not found.
	/// </summary>
	public const string NotFound = "A oportunidade de emprego não foi encontrada.";

	#endregion

	#region 01 - Id

	/// <summary>
	///   Used when the identifier of the job opportunity is empty.
	/// </summary>
	public const string IdCannotBeEmpty = "O identificador da oportunidade de emprego não pode ser vazio.";

	#endregion

	#region 06 - Requirements

	/// <summary>
	///   Used when the requirement of the job opportunity is too short.
	/// </summary>
	public static string RequirementTooShort(int index) =>
		$"O requisito da oportunidade de emprego na posição {index} deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the requirement of the job opportunity is too long.
	/// </summary>
	public static string RequirementTooLong(int index) =>
		$"O requisito da oportunidade de emprego na posição {index} deve ter no máximo 500 caracteres.";

	#endregion

	#region 02 - Name

	/// <summary>
	///   Used when the name of the job opportunity is too short.
	/// </summary>
	public const string NameTooShort = "O nome da oportunidade de emprego deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the name of the job opportunity is too long.
	/// </summary>
	public const string NameTooLong = "O nome da oportunidade de emprego deve ter no máximo 64 caracteres.";

	#endregion

	#region 03 - Description

	/// <summary>
	///   Used when the description of the job opportunity is too short.
	/// </summary>
	public const string DescriptionTooShort = "A descrição da oportunidade de emprego deve ter no mínimo 10 caracteres.";

	/// <summary>
	///   Used when the description of the job opportunity is too long.
	/// </summary>
	public const string DescriptionTooLong = "A descrição da oportunidade de emprego deve ter no máximo 500 caracteres.";

	#endregion

	#region 04 - Location

	/// <summary>
	///   Used when the location city name of the job opportunity is too short.
	/// </summary>
	public const string LocationCityNameTooShort =
		"O nome da cidade da oportunidade de emprego deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the location city name of the job opportunity is too long.
	/// </summary>
	public const string LocationCityNameTooLong =
		"O nome da cidade da oportunidade de emprego deve ter no máximo 32 caracteres.";

	/// <summary>
	///   Used when the location state name of the job opportunity is too short.
	/// </summary>
	public const string LocationStateNameTooShort =
		"O nome do estado da oportunidade de emprego deve ter no mínimo 2 caracteres.";

	/// <summary>
	///   Used when the location state name of the job opportunity is too long.
	/// </summary>
	public const string LocationStateNameTooLong =
		"O nome do estado da oportunidade de emprego deve ter no máximo 2 caracteres.";

	/// <summary>
	///   Used when the location is null for on-site job opportunities.
	/// </summary>
	public const string LocationCantBeNullOnSiteOrHybrid =
		"A localização não pode ser nula para oportunidades de emprego presenciais.";

	#endregion

	#region 05 - Contract

	/// <summary>
	///   Used when the minimum salary of the job opportunity is invalid.
	/// </summary>
	public const string MinSalaryInvalid = "O salário mínimo da oportunidade de emprego é inválido.";

	/// <summary>
	///   Used when the maximum salary of the job opportunity is invalid.
	/// </summary>
	public const string MaxSalaryInvalid = "O salário máximo da oportunidade de emprego é inválido.";

	/// <summary>
	///   Used when the minimum salary is greater than the maximum salary.
	/// </summary>
	public const string MinSalaryGreaterThanMaxSalary = "O salário mínimo não pode ser maior que o salário máximo.";

	#endregion
}