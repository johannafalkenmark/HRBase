using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRBaseWebApp.Controllers;


[Route("api/[controller]")]

[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{

    private readonly IEmployeeService _employeeService = employeeService;

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeRegistrationForm form)
    {
        if (!ModelState.IsValid || form == null)
           return BadRequest("Invalid form data.");
        
        var result = await _employeeService.CreateEmployeeAsync(form);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }
[HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _employeeService.GetAllEmployeesAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        {
        if (id <= 0)
            return BadRequest("Invalid employee ID.");
        var result = await _employeeService.GetEmployeeByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeRegistrationForm form)
    {
        if (id <= 0 || !ModelState.IsValid || form == null)
            return BadRequest("Invalid input data.");
        
        var result = await _employeeService.UpdateEmployeeAsync(id, form);
        return result.IsSuccess ? Ok("Employee updated successfully.") : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid employee ID.");
        
        var result = await _employeeService.DeleteEmployeeAsync(id);
        return result.IsSuccess ? Ok("Employee deleted successfully.") : NotFound(result.Message);
    }
}
