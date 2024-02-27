// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Linq.Expressions;
using Hyre.Shared.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Hyre.Shared.Infrastructure.Repositories;

/// <summary>
///   This class is the base for all repositories.
/// </summary>
/// <inheritdoc cref="IRepositoryBase{T}" />
internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
	private readonly DbContext _context;

	/// <summary>
	///   Initializes a new instance of the <see cref="RepositoryBase{T}" /> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	protected RepositoryBase(DbContext context) => _context = context;

	/// <summary>
	///   This is used to query the database for all entities of type <typeparamref name="T" />.
	/// </summary>
	/// <param name="trackChanges">Should EF Core track changes?</param>
	/// <returns>It will return an IQueryable of <typeparamref name="T" /> type.</returns>
	public IQueryable<T> FindAll(bool trackChanges) => !trackChanges
		? _context.Set<T>().AsNoTracking()
		: _context.Set<T>();

	/// <summary>
	///   This is used to query the database for entities of type <typeparamref name="T" /> that match the given expression.
	/// </summary>
	/// <param name="expression">The expression to filter the entities.</param>
	/// <param name="trackChanges">Should EF Core track changes?</param>
	/// <returns>If the expression is matched, it will return an IQueryable of <typeparamref name="T" /> type.</returns>
	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges
		? _context.Set<T>().Where(expression).AsNoTracking()
		: _context.Set<T>().Where(expression);

	/// <summary>
	///   This method will insert a new entity to the context.
	/// </summary>
	/// <param name="entity">The entity to be inserted.</param>
	public void Insert(T entity) => _context.Set<T>().Add(entity);

	/// <summary>
	///   This method will edit an entity in the context.
	/// </summary>
	/// <param name="entity">The entity to be edited.</param>
	public void Edit(T entity) => _context.Set<T>().Update(entity);

	/// <summary>
	///   This method will remove an entity from the context.
	/// </summary>
	/// <param name="entity">The entity to be removed.</param>
	public void Remove(T entity) => _context.Set<T>().Remove(entity);
}