using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRBaseWebApp.Controllers;

[Route("api/[controller]")]

[ApiController]
public class EmploymentController(IEmploymentService employmentService) : ControllerBase
{

    private readonly IEmploymentService _employmentService = employmentService;

    [HttpPost]
    public async Task<IActionResult> Create(EmploymentRegistrationForm form)
    {
        if (!ModelState.IsValid || form == null)
            return BadRequest("Invalid form data.");

        var result = await _employmentService.CreateEmploymentAsync(form);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _employmentService.GetAllEmploymentsAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid employment ID.");
        var result = await _employmentService.GetEmploymentByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmploymentRegistrationForm form)
    {
        if (id <= 0 || !ModelState.IsValid || form == null)
            return BadRequest("Invalid input data.");

        var result = await _employmentService.UpdateEmploymentAsync(id, form);
        return result.IsSuccess ? Ok("Employment updated successfully.") : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid employment ID.");

        var result = await _employmentService.DeleteEmploymentAsync(id);
        return result.IsSuccess ? Ok("Employment deleted successfully.") : NotFound(result.Message);
    }
}

