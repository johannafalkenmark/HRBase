
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Models.ResponseModels;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository, IEmploymentRepository employmentRepository) : IEmployeeService
{

    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IEmploymentRepository _employmentRepository = employmentRepository;




    //CREATE - Lägger till en ny anställd i databasen

    public async Task<ResponseResult<Employee>> CreateEmployeeAsync(EmployeeRegistrationForm employeeRegistrationForm)
    {

        if (employeeRegistrationForm == null)
        {
            return new ResponseResult<Employee> { IsSuccess = false, Message = "Registration form is null" };
        }

        try
        {
            var employeeBasicEntity = EmployeeFactory.Create(employeeRegistrationForm);
            if (employeeBasicEntity == null)
                return new ResponseResult<Employee> { IsSuccess = false, Message = "Failed to create entity." };


            var result = await _employeeRepository.CreateAsync(employeeBasicEntity);

            if (!result) return new ResponseResult<Employee> { IsSuccess = false, Message = "Failed to create employee in database!" };

            var employee = EmployeeFactory.Create(employeeBasicEntity);
            return new ResponseResult<Employee> { IsSuccess = true, Data = employee, Message = "Employee created successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in CreateEmployeeAsync: {ex.Message}");
            return new ResponseResult<Employee> { IsSuccess = false, Message = "Registration form is null" };
        }
    }


    //READ - Hämtar alla anställda från databasen

    public async Task<ResponseResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
    {
        try
        {
            var employeeEntities = await _employeeRepository.GetAllAsync();
            var employees = employeeEntities
                .Select(EmployeeFactory.Create)
                .Where(e => e != null)
                .ToList()!;
            return new ResponseResult<IEnumerable<Employee>>
            {
                IsSuccess = true,
                Data = employees,
                Message = "Employees retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetAllEmployeesAsync: {ex.Message}");
            return new ResponseResult<IEnumerable<Employee>> { IsSuccess = false, Message = "Error retrieving employees." };
        }
    }

    //READ - Hämtar en anställd med ett specifikt id från databasen

    public async Task<ResponseResult<Employee>> GetEmployeeByIdAsync(int id)
    {
        try
        {
            var employeeEntity = await _employeeRepository.GetAsync(e => e.Id == id);

            if (employeeEntity == null)
                return new ResponseResult<Employee> { IsSuccess = false, Message = "Employee not found" };


            var employee = EmployeeFactory.Create(employeeEntity);
            return new ResponseResult<Employee> { IsSuccess = true, Data = employee, Message = "Employee retrieved successfully" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetEmployeeByIdAsync: {ex.Message}");
            return new ResponseResult<Employee> { IsSuccess = false, Message = "Error retrieving employee." };
        }
    }

    //UPDATE - Uppdaterar en anställd i databasen

    public async Task<ResponseResult<bool>> UpdateEmployeeAsync(int id, EmployeeRegistrationForm employeeRegistrationForm)
    {
        try
        {

            var existingEmployeeEntity = await _employeeRepository.GetAsync(e => e.Id == id);
            if (existingEmployeeEntity == null) return new ResponseResult<bool> { IsSuccess = false, Message = "Employee not found", Data = false };

            var updatedEmployeeEntity = EmployeeFactory.Update(existingEmployeeEntity, employeeRegistrationForm);
            if (updatedEmployeeEntity == null) return new ResponseResult<bool> { IsSuccess = false, Message = "Failed to update entity", Data = false };

            var result = await _employeeRepository.UpdateAsync(updatedEmployeeEntity);
            return new ResponseResult<bool> { IsSuccess = result, Data = result, Message = result ? "Employee updated successfullt." : "Failed to update employee" };

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating employee {ex.Message}");
            return new ResponseResult<bool> { IsSuccess = false, Message = "Employee not found", Data = false };
        }


    }

    //DELETE - Tar bort en anställd från databasen

    public async Task<ResponseResult<bool>> DeleteEmployeeAsync(int id)
    {
        try
        {
            var existingEmployeeEntity = await _employeeRepository.GetAsync(e => e.Id == id);
            if (existingEmployeeEntity == null) return new ResponseResult<bool> { IsSuccess = false, Message = "Employee not found", Data = false };

            var result = await _employeeRepository.DeleteAsync(existingEmployeeEntity);
            return new ResponseResult<bool> { IsSuccess = result, Data = result, Message = result ? "Employee updated successfullt." : "Failed to update employee" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting employeee: {ex.Message}");
            return new ResponseResult<bool> { IsSuccess = false, Message = "Employee not found", Data = false };
        }


    }

}



