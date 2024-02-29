// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Diagnostics.CodeAnalysis;
using Hyre.Shared.Abstractions.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace Hyre.Shared.Infrastructure.Logging;

/// <summary>
///   Implementation of the <see cref="ILoggerManager" /> interface.
/// </summary>
[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
internal sealed class LoggerManager : ILoggerManager
{
	private readonly ILogger _logger;

	public LoggerManager(ILogger logger) => _logger = logger;

	public void LogInfo(string message, params object?[] args) => _logger.LogInformation(message, args);

	public void LogWarn(string message, params object?[] args) => _logger.LogWarning(message, args);

	public void LogDebug(string message, params object?[] args) => _logger.LogDebug(message, args);

	public void LogError(string message, params object?[] args) => _logger.LogError(message, args);
}