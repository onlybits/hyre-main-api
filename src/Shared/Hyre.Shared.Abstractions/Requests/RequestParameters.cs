// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Requests;

/// <summary>
///   This is the base class for all request parameters.
/// </summary>
public abstract class RequestParameters
{
	/// <summary>
	///   The maximum of items per page.
	/// </summary>
	private const int MaxPageSize = 50;

	private int _pageSize = 10;

	/// <summary>
	///   Gets or sets the page number.
	/// </summary>
	public int PageNumber { get; set; } = 1;

	/// <summary>
	///   Gets or sets the page size.
	/// </summary>
	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
	}
}