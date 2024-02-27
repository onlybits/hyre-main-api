// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;

/// <summary>
///   Exception thrown when the identifier of a job opportunity has an empty value.
/// </summary>
public sealed class JobOpportunityIdHasEmptyValueException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityIdHasEmptyValueException" /> class.
	/// </summary>
	public JobOpportunityIdHasEmptyValueException() : base(JobOpportunityErrorMessages.IdCannotBeEmpty)
	{
	}
}