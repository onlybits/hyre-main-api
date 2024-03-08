using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using MediatR;

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   Represents the request to list candidates.
/// </summary>
/// <param name="JobOpportunityId">The job opportunity id.</param>
/// <param name="JobOpportunityTrackChanges">Should EF Core track changes for the job opportunity.</param>
/// <param name="Parameters">The parameters to filter the candidates.</param>
public sealed record ListCandidateRequest(
	JobOpportunityId JobOpportunityId,
	bool JobOpportunityTrackChanges,
	CandidateParameters Parameters) : IRequest<ListCandidateResponse>;