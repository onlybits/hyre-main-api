// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Repositories;

/// <summary>
///   Use this interface to manage all repositories.
/// </summary>
public interface IRepositoryManager
{
	/// <summary>
	///   This method will save all changes made in the database.
	/// </summary>
	/// <returns>Returns a task.</returns>
	Task CommitChangesAsync(CancellationToken cancellationToken = default);
}