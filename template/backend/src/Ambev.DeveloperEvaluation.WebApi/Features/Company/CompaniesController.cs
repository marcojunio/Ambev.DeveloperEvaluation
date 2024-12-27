using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;
using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.DeleteCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.GetCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.UpdateCompany;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company;

public class CompaniesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CompaniesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new company
    /// </summary>
    /// <param name="request">The data creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created company details</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCompanyResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCompanyRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<CreateCompanyCommand>(request);

        command.UserId = new Guid(GetCurrentUserId());
        
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCompanyResult>
        {
            Success = true,
            Message = "Company created successfully",
            Data = _mapper.Map<CreateCompanyResult>(response)
        });
    }


    /// <summary>
    /// Delete a company
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The delete company details</returns>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCompanyResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCompanyRequest(id);
            
        var validator = new DeleteCompanyRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<DeleteCompanyCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Company deleted successfully"
        });
    }
    
    /// <summary>
    /// Get a company
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The delete company details</returns>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCompanyResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCompanyRequest(id);
            
        var validator = new GetCompanyRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<GetCompanyCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }


    /// <summary>
    /// Update a company
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update company</returns>
    [HttpPut()]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCompanyResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCompanyRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<UpdateCompanyCommand>(request);
        
        command.UserId = new Guid(GetCurrentUserId());
        
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}