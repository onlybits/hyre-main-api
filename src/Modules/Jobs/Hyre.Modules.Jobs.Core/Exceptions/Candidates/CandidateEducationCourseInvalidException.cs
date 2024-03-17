// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Core.Exceptions.Candidates;

/// <summary>
///   Exception thrown when the course of the candidate's education is not valid.
/// </summary>
public sealed class CandidateEducationCourseInvalidException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateEducationCourseInvalidException" /> class.
	/// </summary>
	public CandidateEducationCourseInvalidException() : base(CandidateErrorMessages.EducationCourseInvalid)
	{
	}
}