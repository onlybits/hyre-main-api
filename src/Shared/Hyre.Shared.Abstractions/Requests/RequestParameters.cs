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
	///   Initializes a new instance of the <see cref="RequestParameters" /> class.
	/// </summary>
	/// <param name="pageNumber">The page number for each request. Default is 1.</param>
	/// <param name="pageSize">The page size for each request. Default is 10.</param>
	protected RequestParameters(int? pageNumber, int? pageSize)
	{
		PageSize = pageSize ?? 10;
		PageNumber = CheckPageNumber(pageNumber);
	}

	/// <summary>
	///   The page number for each request. Default is 1.
	/// </summary>
	public int PageNumber { get; private set; }

	/// <summary>
	///   The page size for each request. Default is 10.
	///   If the value is greater than <see cref="MaxPageSize" />, it will be set to <see cref="MaxPageSize" />.
	///   Otherwise, it will be set to the value.
	/// </summary>
	public int PageSize { get; private set; }


	/// <summary>
	///   This method calculates the page number for each request.
	/// </summary>
	/// <param name="pageNumber">The page number for each request.</param>
	/// <returns>It will return the page number for each request.</returns>
	public static int CheckPageNumber(int? pageNumber)
	{
		pageNumber ??= 1;
		return pageNumber > MaxPageSize ? MaxPageSize : pageNumber.Value;
	}
}