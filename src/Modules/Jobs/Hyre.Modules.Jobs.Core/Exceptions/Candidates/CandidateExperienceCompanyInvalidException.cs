// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Core.Exceptions.Candidates;

/// <summary>
///   Exception thrown when the company of the candidate's experience is not valid.
/// </summary>
public sealed class CandidateExperienceCompanyInvalidException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateExperienceCompanyInvalidException" /> class.
	/// </summary>
	public CandidateExperienceCompanyInvalidException() : base(CandidateErrorMessages.ExperienceCompanyInvalid)
	{
	}
}