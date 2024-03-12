// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Entities;

#endregion

namespace Hyre.Modules.Identity.Core.Constants;

/// <summary>
///   Represents the user roles that are available in the system.
/// </summary>
public abstract class UserRoles
{
	/// <summary>
	/// </summary>
	public static class Administrator
	{
		/// <summary>
		///   Represents the administrator of the system.
		/// </summary>
		public const string Name = "Administrator";

		/// <summary>
		///   Represents the normalized admin role.
		/// </summary>
		public const string Normalized = "ADMINISTRATOR";
	}

	/// <summary>
	///   Represents the user when a candidate is hired.
	/// </summary>
	public static class Employee
	{
		/// <summary>
		///   Represents the employee role.
		/// </summary>
		public const string Name = "Employee";

		/// <summary>
		///   Represents the normalized employee role.
		/// </summary>
		public const string Normalized = "EMPLOYEE";
	}

	/// <summary>
	///   Represents the user created when someone candidates to a job.
	/// </summary>
	public static class Candidate
	{
		/// <summary>
		///   Represents the candidate role.
		/// </summary>
		public const string Name = "Candidate";

		/// <summary>
		///   Represents the normalized candidate role.
		/// </summary>
		public const string Normalized = "CANDIDATE";
	}
}

/// <summary>
///   Error messages that are used for the <see cref="User" /> entity.
/// </summary>
public abstract class UserErrorMessages
{
	#region Application

	/// <summary>
	///   Used when the user already exists.
	/// </summary>
	public const string AlreadyExists = "O usuário com o e-mail informado já existe.";

	#endregion
}