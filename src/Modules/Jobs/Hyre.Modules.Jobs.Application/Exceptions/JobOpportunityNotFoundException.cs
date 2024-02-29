// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Application.Exceptions;

/// <summary>
///   Exception thrown when a job opportunity is not found.
/// </summary>
public sealed class JobOpportunityNotFoundException : NotFoundException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityNotFoundException" /> class.
	/// </summary>
	public JobOpportunityNotFoundException() : base(JobOpportunityErrorMessages.NotFound)
	{
	}
}