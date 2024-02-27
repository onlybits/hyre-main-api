﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Kernel.Entities;

#endregion

namespace Hyre.Modules.Jobs.Core.Entities;

/// <summary>
///   Represents a job opportunity in the system.
/// </summary>
public sealed class JobOpportunity : EntityBase<JobOpportunityId>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <param name="id">The identifier of the job opportunity.</param>
	private JobOpportunity(JobOpportunityId id) : base(id)
	{
	}

	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunity" /> class.
	/// </summary>
	/// <returns>It will return a new instance of the <see cref="JobOpportunity" /> class.</returns>
	public static JobOpportunity Create() => new(JobOpportunityId.New());
}