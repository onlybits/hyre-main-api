using MediatR;

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.List;

/// <summary>
///   This is the use case contract to list candidates.
/// </summary>
internal interface IListCandidateUseCase : IRequestHandler<ListCandidateRequest, ListCandidateResponse>;