// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;

/// <summary>
///   Exception thrown when the location of a job opportunity is null and the location type is on site.
/// </summary>
public sealed class JobOpportunityLocationCantBeNullOnSiteOrHybridException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityLocationCantBeNullOnSiteOrHybridException" /> class.
	/// </summary>
	public JobOpportunityLocationCantBeNullOnSiteOrHybridException() : base(JobOpportunityErrorMessages.LocationCantBeNullOnSiteOrHybrid)
	{
	}
}