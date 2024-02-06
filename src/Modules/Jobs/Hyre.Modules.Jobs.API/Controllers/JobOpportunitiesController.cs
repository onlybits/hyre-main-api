// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Text.Json;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Find;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.List;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Shared.Abstractions.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Hyre.Modules.Jobs.API.Controllers;

/// <summary>
///   This controller is responsible for handling requests related to job opportunities.
/// </summary>
[ApiController]
[Route("api/job-opportunities")]
internal sealed class JobOpportunitiesController : ControllerBase
{
	private readonly ISender _sender;

	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunitiesController" /> class.
	/// </summary>
	/// <param name="sender">The MediatR sender, used to send requests to the application layer.</param>
	public JobOpportunitiesController(ISender sender) => _sender = sender;

	/// <summary>
	///   This endpoint is responsible for listing job opportunities.
	/// </summary>
	/// <param name="parameters">The parameters used to filter the job opportunities.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns a list of job opportunities.</returns>
	[HttpGet]
	[ProducesResponseType<List<JobOpportunity>>(StatusCodes.Status200OK)]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> ListAsync([FromQuery] JobOpportunityParameters parameters, CancellationToken cancellationToken = default)
	{
		var request = new ListJobOpportunityRequest(parameters);
		var response = await _sender.Send(request, cancellationToken);

		Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(response.MetaData));

		return response.JobOpportunities.Any() ? Ok(response.JobOpportunities) : NoContent();
	}

	/// <summary>
	///   This endpoint is responsible for retrieving a job opportunity by its id.
	/// </summary>
	/// <param name="id">The id of the job opportunity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns the job opportunity.</returns>
	[HttpGet("{id:guid}")]
	[ProducesResponseType<JobOpportunity>(StatusCodes.Status200OK)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Find(Guid id, CancellationToken cancellationToken = default)
	{
		var request = new FindJobOpportunityRequest(id, false);
		var response = await _sender.Send(request, cancellationToken);

		return Ok(response);
	}

	/// <summary>
	///   This endpoint is responsible for creating a new job opportunity.
	/// </summary>
	/// <param name="input">The input data used to create the job opportunity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns the created job opportunity.</returns>
	[HttpPost]
	[ProducesResponseType<JobOpportunity>(StatusCodes.Status201Created)]
	public async Task<IActionResult> Create([FromBody] CreateJobOpportunityInput input, CancellationToken cancellationToken = default)
	{
		var request = new CreateJobOpportunityRequest(input);
		var response = await _sender.Send(request, cancellationToken);

		return CreatedAtAction(nameof(Find), new { id = response.Id.Value }, response);
	}

	/// <summary>
	///   This endpoint is responsible for updating a job opportunity.
	/// </summary>
	/// <param name="id">The id of the job opportunity.</param>
	/// <param name="input">The input data used to update the job opportunity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns no content.</returns>
	[HttpPut("{id:guid}")]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateJobOpportunityInput input,
		CancellationToken cancellationToken = default)
	{
		var request = new UpdateJobOpportunityRequest(id, input, true);
		await _sender.Send(request, cancellationToken);

		return NoContent();
	}

	/// <summary>
	///   This endpoint is responsible for deleting a job opportunity.
	/// </summary>
	/// <param name="id">The id of the job opportunity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns no content.</returns>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
	{
		var request = new DeleteJobOpportunityRequest(id, false);
		await _sender.Send(request, cancellationToken);

		return NoContent();
	}
}