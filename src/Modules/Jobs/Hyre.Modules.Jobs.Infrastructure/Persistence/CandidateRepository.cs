// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Modules.Jobs.Infrastructure.Persistence;

/// <summary>
///   This is the repository for the <see cref="Candidate" /> entity.
/// </summary>
/// <inheritdoc cref="ICandidateRepository" />
internal sealed class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateRepository" /> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public CandidateRepository(DbContext context) : base(context)
	{
	}
}