// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Jobs.Core.Enums;

/// <summary>
///   Represents the contract type of a job opportunity.
/// </summary>
public enum ContractType
{
	/// <summary>
	///   When you are learning and working at the same time.
	/// </summary>
	Apprentice,

	/// <summary>
	///   When you are doing a job as part of your studies.
	/// </summary>
	Internship,

	/// <summary>
	///   When you are working with a contract and you are part of the company.
	/// </summary>
	Effective,

	/// <summary>
	///   When you are working with a contract for just a specific period of time.
	/// </summary>
	PartTime
}