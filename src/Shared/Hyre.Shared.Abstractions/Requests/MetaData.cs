// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Requests;

/// <summary>
///   This class represents the metadata of a paginated list.
/// </summary>
public sealed class MetaData
{
	/// <summary>
	///   Initializes a new instance of the <see cref="MetaData" /> class.
	/// </summary>
	/// <param name="currentPage"></param>
	/// <param name="pageSize"></param>
	/// <param name="totalCount"></param>
	public MetaData(int currentPage, int pageSize, int totalCount)
	{
		CurrentPage = currentPage;
		PageSize = pageSize;
		TotalCount = totalCount;
		TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
	}

	/// <summary>
	///   Gets or sets the current page.
	/// </summary>
	public int CurrentPage { get; }

	/// <summary>
	///   Gets or sets the total number of pages.
	/// </summary>
	public int TotalPages { get; }

	/// <summary>
	///   Gets or sets the page size.
	/// </summary>
	public int PageSize { get; private set; }

	/// <summary>
	///   Gets or sets the total count.
	/// </summary>
	public int TotalCount { get; private set; }

	/// <summary>
	///   Gets a value indicating whether we have a previous page.
	/// </summary>
	public bool HasPrevious => CurrentPage > 1;

	/// <summary>
	///   Gets a value indicating whether we have a next page.
	/// </summary>
	public bool HasNext => CurrentPage < TotalPages;
}