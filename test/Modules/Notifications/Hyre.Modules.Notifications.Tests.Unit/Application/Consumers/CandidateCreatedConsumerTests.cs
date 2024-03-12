// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Notifications.Application.Consumers;
using Hyre.Modules.Notifications.Core.Repositories;
using Hyre.Modules.Notifications.Tests.Unit.Common;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;
using NSubstitute;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Application.Consumers;

/// <summary>
///   Unit tests for the candidate created consumer.
/// </summary>
public sealed class CandidateCreatedConsumerTests : NotificationBaseFixture
{
	private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
	private readonly INotificationsRepositoryManager _repository = Substitute.For<INotificationsRepositoryManager>();

	[Fact(DisplayName = nameof(Consume_WhenCalled_ShouldLogInfo))]
	[Trait(ConsumersTraits.Name, ConsumersTraits.Value)]
	public async Task Consume_WhenCalled_ShouldLogInfo()
	{
		// Arrange
		var email = Faker.Internet.Email();
		var candidateCreatedEvent = new CandidateCreatedEvent(email);
		var consumerContext = Substitute.For<ConsumeContext<CandidateCreatedEvent>>();
		var consumer = new CandidateCreatedConsumer(_repository, _logger);

		// Act
		_ = consumerContext.Message.Returns(candidateCreatedEvent);
		await consumer.Consume(consumerContext);

		// Assert
		_logger
			.Received(1)
			.LogInfo("Consuming message for candidate with email: {Email}", Arg.Any<string>());
	}
}