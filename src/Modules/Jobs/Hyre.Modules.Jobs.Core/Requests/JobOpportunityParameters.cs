// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Core.Requests;

/// <summary>
///   Parameters for the job opportunity listing.
/// </summary>
public sealed class JobOpportunityParameters : RequestParameters
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityParameters" /> class.
	/// </summary>
	/// <param name="pageNumber">The page number for each request.</param>
	/// <param name="pageSize">The page size for each request.</param>
	public JobOpportunityParameters(int? pageNumber, int? pageSize) : base(pageNumber, pageSize)
	{
	}
}