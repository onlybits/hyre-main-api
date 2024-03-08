using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Shared.Abstractions.Requests;

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   Represents the response to list candidates.
/// </summary>
/// <param name="MetaData">The metadata of the paged list.</param>
/// <param name="Candidates">The list of candidates.</param>
public sealed record ListCandidateResponse(
	MetaData MetaData,
	IEnumerable<Candidate> Candidates);