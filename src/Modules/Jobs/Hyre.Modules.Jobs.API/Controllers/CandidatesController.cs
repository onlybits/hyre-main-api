// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Text.Json;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Delete;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.List;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Requests;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Hyre.Modules.Jobs.API.Controllers;

/// <summary>
///   This controller is responsible for handling requests related to candidates.
/// </summary>
[ApiController]
[Route("api/job-opportunities/{jobOpportunityIdValue:guid}/candidates")]
internal sealed class CandidatesController : ControllerBase
{
	private readonly ISender _sender;

	/// <summary>
	///   Initializes a new instance of the <see cref="CandidatesController" /> class.
	/// </summary>
	/// <param name="sender">The MediatR sender, used to send requests to the application layer.</param>
	public CandidatesController(ISender sender) => _sender = sender;

	/// <summary>
	///   This endpoint is responsible for listing candidates for a specific job opportunity.
	/// </summary>
	/// <param name="jobOpportunityIdValue">The id of the job opportunity.</param>
	/// <param name="parameters">The parameters used to filter the candidates.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns a list of candidates.</returns>
	[HttpGet]
	[ProducesResponseType<List<Candidate>>(StatusCodes.Status200OK)]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> ListAsync(
		[FromRoute] Guid jobOpportunityIdValue,
		[FromQuery] CandidateParameters parameters,
		CancellationToken cancellationToken = default)
	{
		var jobOpportunityId = new JobOpportunityId(jobOpportunityIdValue);
		var request = new ListCandidateRequest(jobOpportunityId, false, parameters);
		var response = await _sender.Send(request, cancellationToken);

		Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(response.MetaData));

		return response.Candidates.Any() ? Ok(response.Candidates) : NoContent();
	}

	/// <summary>
	///   This endpoint is responsible for retrieving a candidate by its id.
	/// </summary>
	/// <param name="jobOpportunityIdValue">The id of the job opportunity.</param>
	/// <param name="candidateIdValue">The id of the candidate.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns the candidate.</returns>
	[HttpGet("{candidateIdValue:guid}")]
	[ProducesResponseType<Candidate>(StatusCodes.Status200OK)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Find(
		[FromRoute] Guid jobOpportunityIdValue,
		[FromRoute] Guid candidateIdValue,
		CancellationToken cancellationToken = default)
	{
		var jobOpportunityId = new JobOpportunityId(jobOpportunityIdValue);
		var candidateId = new CandidateId(candidateIdValue);
		var request = new FindCandidateRequest(jobOpportunityId, candidateId, false);
		var response = await _sender.Send(request, cancellationToken);

		return Ok(response);
	}

	/// <summary>
	///   This endpoint is responsible for creating a new candidate.
	/// </summary>
	/// <param name="jobOpportunityIdValue">The id of the job opportunity.</param>
	/// <param name="input">The input data used to create the candidate.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns the created candidate.</returns>
	[HttpPost]
	[ProducesResponseType<Candidate>(StatusCodes.Status201Created)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create([FromRoute] Guid jobOpportunityIdValue, [FromBody] CreateCandidateInput input,
		CancellationToken cancellationToken = default)
	{
		var jobOpportunityId = new JobOpportunityId(jobOpportunityIdValue);
		var request = new CreateCandidateRequest(jobOpportunityId, input, false);
		var response = await _sender.Send(request, cancellationToken);

		return CreatedAtAction(nameof(Find), new { jobOpportunityIdValue, candidateIdValue = response.Id.Value }, response);
	}

	/// <summary>
	///   This endpoint is responsible for updating a candidate.
	/// </summary>
	/// <param name="jobOpportunityIdValue">The id of the job opportunity.</param>
	/// <param name="candidateIdValue">The id of the candidate.</param>
	/// <param name="input">The input data used to update the candidate.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns no content.</returns>
	[HttpPut("{candidateIdValue:guid}")]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update([FromRoute] Guid jobOpportunityIdValue, [FromRoute] Guid candidateIdValue,
		[FromBody] UpdateCandidateInput input, CancellationToken cancellationToken = default)
	{
		var jobOpportunityId = new JobOpportunityId(jobOpportunityIdValue);
		var candidateId = new CandidateId(candidateIdValue);
		var request = new UpdateCandidateRequest(jobOpportunityId, candidateId, input, false);
		await _sender.Send(request, cancellationToken);

		return NoContent();
	}

	/// <summary>
	///   This endpoint is responsible for deleting a candidate.
	/// </summary>
	/// <param name="jobOpportunityIdValue">The id of the job opportunity.</param>
	/// <param name="candidateIdValue">The id of the candidate.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Returns no content.</returns>
	[HttpDelete("{candidateIdValue:guid}")]
	[ProducesResponseType<NoContent>(StatusCodes.Status204NoContent)]
	[ProducesResponseType<NotFoundException>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Delete([FromRoute] Guid jobOpportunityIdValue, [FromRoute] Guid candidateIdValue,
		CancellationToken cancellationToken = default)
	{
		var jobOpportunityId = new JobOpportunityId(jobOpportunityIdValue);
		var candidateId = new CandidateId(candidateIdValue);
		var request = new DeleteCandidateRequest(jobOpportunityId, candidateId, false);
		await _sender.Send(request, cancellationToken);

		return NoContent();
	}
}