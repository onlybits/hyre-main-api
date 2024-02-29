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

	#region 02 - Name

	/// <summary>
	///   Used when the name of the job opportunity is too short.
	/// </summary>
	public const string NameTooShort = "O nome da oportunidade de emprego deve ter no mínimo 3 caracteres.";

	/// <summary>
	///   Used when the name of the job opportunity is too long.
	/// </summary>
	public const string NameTooLong = "O nome da oportunidade de emprego deve ter no máximo 32 caracteres.";

	#endregion
}