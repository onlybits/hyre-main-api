// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Jobs.Core.Enums;

/// <summary>
///   Represents the location type of a job opportunity.
/// </summary>
public enum LocationType
{
	/// <summary>
	///   When you can work from anywhere and don't need to be in a specific location.
	/// </summary>
	Remote,

	/// <summary>
	///   When you can work some days from anywhere and some days from a specific location.
	/// </summary>
	Hybrid,

	/// <summary>
	///   When you need to be in a specific location to work every day.
	/// </summary>
	OnSite
}