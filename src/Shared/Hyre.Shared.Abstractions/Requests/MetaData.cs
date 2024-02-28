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
	///   Gets or sets the current page.
	/// </summary>
	public int CurrentPage { get; set; }

	/// <summary>
	///   Gets or sets the total number of pages.
	/// </summary>
	public int TotalPages { get; set; }

	/// <summary>
	///   Gets or sets the page size.
	/// </summary>
	public int PageSize { get; set; }

	/// <summary>
	///   Gets or sets the total count.
	/// </summary>
	public int TotalCount { get; set; }

	/// <summary>
	///   Gets a value indicating whether we have a previous page.
	/// </summary>
	public bool HasPrevious => CurrentPage > 1;

	/// <summary>
	///   Gets a value indicating whether we have a next page.
	/// </summary>
	public bool HasNext => CurrentPage < TotalPages;
}