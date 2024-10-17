using System.Net.Mime;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Services;
using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace AidManager.API.Authentication.Interfaces;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CompanyController(ICompanyCommandService companyCommandService, ICompanyQueryService companyQueryService) : ControllerBase
{
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var query = new GetCompanyByUserIdQuery(id);
        var company = await companyQueryService.Handle(query);
        if (company == null) return Ok(new { status_code = 404, message = "Company not found" });
        return Ok(new { status_code = 200, message = "Company found", company = CreateCompanyResourceFromEntityAssembler.ToResourceFromEntity(company) });
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyResource resource)
    {
        var command = UpdateCompanyCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await companyCommandService.Handle(command);
        if (!result) return Ok(new { status_code = 404, message = "Company not found" });
        return Ok(new { status_code = 200, message = "Company updated" });
    }
    
}