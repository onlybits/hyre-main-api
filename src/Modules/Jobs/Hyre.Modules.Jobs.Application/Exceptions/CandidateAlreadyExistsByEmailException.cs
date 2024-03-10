// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Application.Exceptions;

/// <summary>
///   Initializes a new instance of the <see cref="CandidateAlreadyExistsByEmailException" /> class.
/// </summary>
public sealed class CandidateAlreadyExistsByEmailException : ConflictException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateAlreadyExistsByEmailException" /> class.
	/// </summary>
	public CandidateAlreadyExistsByEmailException() : base(CandidateErrorMessages.AlreadyExistsByEmail)
	{
	}
}