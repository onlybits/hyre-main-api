// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   Represents the response to list candidates.
/// </summary>
/// <param name="MetaData">The metadata of the paged list.</param>
/// <param name="Candidates">The list of candidates.</param>
public sealed record ListCandidateResponse(
	MetaData MetaData,
	IReadOnlyList<Candidate> Candidates);