// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's address.
/// </summary>
public sealed record CandidateAddress : ValueObject
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateAddress" /> class.
	/// </summary>
	/// <param name="country">The country where the candidate lives.</param>
	/// <param name="state">The state where the candidate lives.</param>
	/// <param name="city">The city where the candidate lives.</param>
	/// <param name="neighborhood">The neighborhood where the candidate lives.</param>
	/// <param name="complement">The complement of the address.</param>
	/// <param name="number">The number of the address.</param>
	/// <param name="zipCode">The zip code of the address.</param>
	public CandidateAddress(
		string country,
		string state,
		string city,
		string neighborhood,
		string? complement,
		int number,
		string zipCode)
	{
		Country = country;
		State = state;
		City = city;
		Neighborhood = neighborhood;
		Complement = complement;
		Number = number;
		ZipCode = zipCode;
		Validate();
	}

	/// <summary>
	///   Gets the country where the candidate lives.
	/// </summary>
	public string Country { get; }

	/// <summary>
	///   Gets the state where the candidate lives.
	/// </summary>
	public string State { get; }

	/// <summary>
	///   Gets the city where the candidate lives.
	/// </summary>
	public string City { get; }

	/// <summary>
	///   Gets the neighborhood where the candidate lives.
	/// </summary>
	public string Neighborhood { get; }

	/// <summary>
	///   Gets the complement of the address.
	/// </summary>
	public string? Complement { get; }

	/// <summary>
	///   Gets the number of the address.
	/// </summary>
	public int Number { get; }

	/// <summary>
	///   Gets the zip code of the address.
	/// </summary>
	public string ZipCode { get; }

	/// <summary>
	///   This method validates the <see cref="CandidateAddress" />.
	/// </summary>
	private void Validate()
	{
		if (ZipCode.Length != 8)
		{
			throw new CandidateAddressZipCodeNotValidException();
		}

		if (Country.Length is < 3 or > 50)
		{
			throw new CandidateAddressCountryNotValidException();
		}

		if (State.Length is < 3 or > 50)
		{
			throw new CandidateAddressStateNotValidException();
		}

		if (City.Length is < 3 or > 50)
		{
			throw new CandidateAddressCityNotValidException();
		}

		if (Complement?.Length is < 3 or > 100)
		{
			throw new CandidateAddressComplementNotValidException();
		}

		if (Neighborhood.Length is < 3 or > 100)
		{
			throw new CandidateAddressNeighborhoodNotValidException();
		}

		if (Number <= 0)
		{
			throw new CandidateAddressNumberNotValidException();
		}
	}
}