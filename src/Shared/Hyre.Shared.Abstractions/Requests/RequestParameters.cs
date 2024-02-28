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
	///   Maximum page size for each request.
	/// </summary>
	private const int MaxPageSize = 50;

	/// <summary>
	///   The default page size for each request.
	/// </summary>
	private int _pageSize = 10;

	/// <summary>
	///   The page number for each request. Default is 1.
	/// </summary>
	public int PageNumber { get; set; } = 1;

	/// <summary>
	///   The page size for each request. Default is 10.
	///   If the value is greater than <see cref="MaxPageSize" />, it will be set to <see cref="MaxPageSize" />.
	///   Otherwise, it will be set to the value.
	/// </summary>
	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
	}
}