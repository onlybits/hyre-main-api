// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Linq.Expressions;

#endregion

namespace Hyre.Shared.Abstractions.Repositories;

/// <summary>
///   This interface will be used to define the base methods for all repositories.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IRepositoryBase<T>
{
	/// <summary>
	///   This is used to query the database for all entities of type <typeparamref name="T" />.
	/// </summary>
	/// <param name="trackChanges">Should EF Core track changes?</param>
	/// <returns>Returns an IQueryable of <typeparamref name="T" /> type.</returns>
	IQueryable<T> FindAll(bool trackChanges);

	/// <summary>
	///   This method will return a list of <typeparamref name="T" /> type, that match the given expression.
	/// </summary>
	/// <param name="expression">The expression to filter the entities.</param>
	/// <param name="trackChanges">Should EF Core track changes?</param>
	/// <returns>Returns an IQueryable of <typeparamref name="T" /> type.</returns>
	IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

	/// <summary>
	///   This method will insert a new entity to the context.
	/// </summary>
	/// <param name="entity">The entity to be inserted.</param>
	void Insert(T entity);

	/// <summary>
	///   This method will update an entity in the context.
	/// </summary>
	/// <param name="entity">The entity to be updated.</param>
	void Edit(T entity);

	/// <summary>
	///   This method will delete an entity from the context.
	/// </summary>
	/// <param name="entity">The entity to be deleted.</param>
	void Remove(T entity);
}