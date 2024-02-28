// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Requests;

/// <summary>
///   Represents a list with pagination data.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public sealed class PagedList<T> : List<T>
{
	/// <summary>
	///   Initializes a new instance of the <see cref="PagedList{T}" /> class.
	/// </summary>
	/// <param name="items">The items to be paged.</param>
	/// <param name="count">The total number of items.</param>
	/// <param name="pageNumber">The current page number.</param>
	/// <param name="pageSize">The page size.</param>
	public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
	{
		MetaData = new MetaData
		{
			TotalCount = count,
			PageSize = pageSize,
			CurrentPage = pageNumber,
			TotalPages = (int)Math.Ceiling(count / (double)pageSize)
		};
		AddRange(items);
	}

	/// <summary>
	///   Gets or sets the metadata values.
	/// </summary>
	public MetaData MetaData { get; set; }

	/// <summary>
	///   Creates a new instance of <see cref="PagedList{T}" />.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <returns>Returns a new instance of <see cref="PagedList{T}" />.</returns>
	public static PagedList<T> ToPagedList(
		IList<T> source,
		int pageNumber,
		int pageSize)
	{
		var count = source.Count;
		var items = source
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToList();

		return new PagedList<T>(items, count, pageNumber, pageSize);
	}
}