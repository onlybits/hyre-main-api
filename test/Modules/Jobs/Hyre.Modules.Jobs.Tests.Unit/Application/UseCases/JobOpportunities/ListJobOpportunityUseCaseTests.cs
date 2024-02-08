// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;
using Hyre.Modules.Jobs.Core.Repositories;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Shared.Abstractions.Logging;
using NSubstitute;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="ListJobOpportunityUseCase" /> class.
/// </summary>
public sealed class ListJobOpportunityUseCaseTests : ListJobOpportunityUseCaseTestsFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly IJobsRepositoryManager _repository = Substitute.For<IJobsRepositoryManager>();
	private readonly ListJobOpportunityUseCase _sut;

	public ListJobOpportunityUseCaseTests() => _sut = new ListJobOpportunityUseCase(_repository, _logger);

	[Fact(DisplayName = nameof(Handle_WhenGivenValidInput_ShouldListJobOpportunities))]
	[Trait(UseCaseTraits.Name, UseCaseTraits.Value)]
	public void Handle_WhenGivenValidInput_ShouldListJobOpportunities()
	{
		// Arrange
		var parameters = new JobOpportunityParameters
		{
			PageSize = 1,
			PageNumber = 10
		};

		var response = GenerateValidPagedList(parameters.PageSize, parameters.PageNumber, 10);
		var request = new ListJobOpportunityRequest(parameters);

		_ = _repository.JobOpportunity.ListAsync(Arg.Any<JobOpportunityParameters>(), CancellationToken.None).Returns(response);

		// Act
		var result = _sut.Handle(request, CancellationToken.None);

		// Assert
		_ = result.Should().NotBeNull();

		_logger.Received(1).LogInfo(
			Arg.Is("{Count} Job opportunities listed successfully."),
			Arg.Any<object?[]>());
	}
}