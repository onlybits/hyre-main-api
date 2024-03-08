using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.List;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using Xunit;

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.Candidates;

/// <summary>
///   Unit tests for the <see cref="ListCandidateUseCase" /> class.
/// </summary>
public sealed class ListCandidatesUseCaseTests : CandidateBaseFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly ListCandidateUseCase _sut;

	public ListCandidatesUseCaseTests() => _sut = new ListCandidateUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidRequest_ShouldListCandidates))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public void Handle_WhenGivenValidRequest_ShouldListCandidates()
	{
		// Arrange
		var candidates = GeneratePagedListCandidates(1, 10, 10);
		var parameters = new CandidateParameters
		{
			PageSize = 1,
			PageNumber = 10
		};

		var request = new ListCandidateRequest(JobOpportunityId.New(), false, parameters);

		_ = _repository
			.JobOpportunity
			.ExistsAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CancellationToken>())
			.Returns(true);

		_ = _repository
			.Candidate
			.ListAsync(Arg.Any<JobOpportunityId>(), Arg.Any<CandidateParameters>(), Arg.Any<CancellationToken>())
			.Returns(candidates);

		// Act
		var result = _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();

		_logger.Received(1).LogInfo(
			Arg.Is("{Count} Candidates listed successfully."),
			Arg.Any<object?[]>());
	}
}