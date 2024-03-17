// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.Exceptions;
using Hyre.Modules.Identity.Application.Services;
using Hyre.Modules.Identity.Core.Constants;
using Hyre.Modules.Identity.Core.Entities;
using Hyre.Modules.Jobs.Core.Events;
using Hyre.Shared.Abstractions.Logging;
using MassTransit;

#endregion

namespace Hyre.Modules.Identity.Application.Consumers;

/// <summary>
///   This consumer is responsible for creating a user when a candidate is created.
/// </summary>
public sealed class CandidateCreatedConsumer : IConsumer<CandidateCreatedEvent>
{
	private readonly IIdentityService _identityService;
	private readonly ILoggerManager _logger;

	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateCreatedConsumer" /> class.
	/// </summary>
	/// <param name="identityService">The identity service.</param>
	/// <param name="logger">The logger manager.</param>
	public CandidateCreatedConsumer(
		IIdentityService identityService,
		ILoggerManager logger)
	{
		_identityService = identityService;
		_logger = logger;
	}

	public async Task Consume(ConsumeContext<CandidateCreatedEvent> context)
	{
		var finalEmail = context.Message.Email;
		var user = User.Create(finalEmail);

		// We need to check if the user already exists.
		// If it does, we throw an exception, so the notification module can handle it.
		// It will send an email to the user informing that the account already exists.
		var userExists = await _identityService.UserExistsAsync(finalEmail);
		if (userExists)
		{
			_logger.LogError("User with email {finalEmail} already exists.", finalEmail);
			throw new UserAlreadyExistsException();
		}

		var userCreated = await _identityService.CreateUserAsync(
			user,
			context.Message.Document,
			UserRoles.Candidate.Name);

		if (!userCreated)
		{
			_logger.LogError("User with email {finalEmail} could not be created.", finalEmail);
		}
		else
		{
			_logger.LogInfo("User with email {finalEmail} created successfully.", finalEmail);
		}
	}
}