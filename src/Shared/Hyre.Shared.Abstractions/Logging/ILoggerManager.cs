// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Logging;

/// <summary>
///   This interface represents a logger manager.
/// </summary>
/// <typeparam name="TType">The type of the logger manager.</typeparam>
public interface ILoggerManager<TType>
{
	/// <summary>
	///   Log a information message.
	/// </summary>
	/// <param name="message">The message to be logged.</param>
	/// <param name="args">The arguments to be formatted into the message.</param>
	void LogInfo(string message, params object?[] args);

	/// <summary>
	///   Log a warning message.
	/// </summary>
	/// <param name="message">The message to be logged.</param>
	/// <param name="args">The arguments to be formatted into the message.</param>
	void LogWarn(string message, params object?[] args);

	/// <summary>
	///   Log a debug message.
	/// </summary>
	/// <param name="message">The message to be logged.</param>
	/// <param name="args">The arguments to be formatted into the message.</param>
	void LogDebug(string message, params object?[] args);

	/// <summary>
	///   Log an error message.
	/// </summary>
	/// <param name="exception">The exception to be logged.</param>
	/// <param name="message">The message to be logged.</param>
	/// <param name="args">The arguments to be formatted into the message.</param>
	void LogError(Exception exception, string message, params object?[] args);
}