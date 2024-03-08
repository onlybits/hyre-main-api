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
}