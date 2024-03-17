// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Core.Exceptions.Candidates;

/// <summary>
///   Exception thrown when the start date of the candidate's education is not valid.
/// </summary>
public sealed class CandidateEducationStartDateInvalidException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateEducationStartDateInvalidException" /> class.
	/// </summary>
	public CandidateEducationStartDateInvalidException() : base(CandidateErrorMessages.EducationStartDateInvalid)
	{
	}
}