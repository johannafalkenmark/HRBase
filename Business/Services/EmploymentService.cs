using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Models.ResponseModels;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class EmploymentService(IEmploymentRepository employmentRepository) : IEmploymentService
{
    private readonly IEmploymentRepository _employmentRepository = employmentRepository;
    // CREATE - Lägger till en ny anställning i databasen
    public async Task<ResponseResult<Employment>> CreateEmploymentAsync(EmploymentRegistrationForm employmentForm)
    {
        if (employmentForm == null)
        {
            return new ResponseResult<Employment> { IsSuccess = false, Message = "Employment form is null" };
        }
        try
        {
            var employmentEntity = EmploymentFactory.Create(employmentForm);
            if (employmentEntity == null)
                return new ResponseResult<Employment> { IsSuccess = false, Message = "Failed to create entity." };
            var result = await _employmentRepository.CreateAsync(employmentEntity);
            if (!result) return new ResponseResult<Employment> { IsSuccess = false, Message = "Failed to create employment in database." };

            var employment = EmploymentFactory.Create(employmentEntity);
            return new ResponseResult<Employment> { IsSuccess = true, Data = employment, Message = "Employment created successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in CreateEmploymentAsync: {ex.Message}");
            return new ResponseResult<Employment> { IsSuccess = false, Message = "An error occurred while creating employment." };
        }
    }


    // READ - Hämtar alla anställningar från databasen
    public async Task<ResponseResult<IEnumerable<Employment>>> GetAllEmploymentsAsync()
    {
        try
        {
            var employmentEntities = await _employmentRepository.GetAllAsync();
            var employments = employmentEntities
                .Select(EmploymentFactory.Create)
                .Where(e => e != null)
                .ToList()!;
            return new ResponseResult<IEnumerable<Employment>> { IsSuccess = true, Data = employments, Message = "Employments retrieved successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetAllEmploymentsAsync: {ex.Message}");
            return new ResponseResult<IEnumerable<Employment>> { IsSuccess = false, Message = "An error occurred while retrieving employments." };
        }
    }

    public async Task<ResponseResult<Employment>> GetEmploymentByIdAsync(int id)
    {
        if (id <= 0)
        {
            return new ResponseResult<Employment> { IsSuccess = false, Message = "Invalid employment ID." };
        }
        try
        {
            var employmentEntity = await _employmentRepository.GetAsync(e => e.Id == id);
            if (employmentEntity == null)
            {
                return new ResponseResult<Employment> { IsSuccess = false, Message = "Employment not found." };
            }
            var employment = EmploymentFactory.Create(employmentEntity);
            return new ResponseResult<Employment> { IsSuccess = true, Data = employment, Message = "Employment retrieved successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetEmploymentByIdAsync: {ex.Message}");
            return new ResponseResult<Employment> { IsSuccess = false, Message = "An error occurred while retrieving the employment." };
        }
    }


    //UPDATE - Uppdaterar en anställning i databasen
    public async Task<ResponseResult<bool>> UpdateEmploymentAsync(int id, EmploymentRegistrationForm employmentForm)
    {
        if (id <= 0 || employmentForm == null)
        {
            return new ResponseResult<bool> { IsSuccess = false, Message = "Invalid id or employment null." };
        }
        try
        {
            var existingEntity = await _employmentRepository.GetAsync(e => e.Id == id);
            if (existingEntity == null)
            {
                return new ResponseResult<bool> { IsSuccess = false, Message = "Employment not found." };
            }
            var updatedEntity = EmploymentFactory.Update(existingEntity, employmentForm);
            if (updatedEntity == null)
            {
                return new ResponseResult<bool> { IsSuccess = false, Message = "Failed to update entity." };
            }
            var result = await _employmentRepository.UpdateAsync(updatedEntity);
            if (!result)
            {
                return new ResponseResult<bool> { IsSuccess = false, Message = "Failed to update employment in database." };
            }
            return new ResponseResult<bool> { IsSuccess = true, Data = true, Message = "Employment updated successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in UpdateEmploymentAsync: {ex.Message}");
            return new ResponseResult<bool> { IsSuccess = false, Message = "An error occurred while updating the employment." };
        }
    }

    //DELETE - Tar bort en anställning från databasen

    public async Task<ResponseResult<bool>> DeleteEmploymentAsync(int id)
    {
        if (id <= 0)
        {
            return new ResponseResult<bool> { IsSuccess = false, Message = "Invalid employment ID." };
        }
        try
        {
            var existingEntity = await _employmentRepository.GetAsync(e => e.Id == id);
            if (existingEntity == null)
            {
                return new ResponseResult<bool> { IsSuccess = false, Message = "Employment not found." };
            }
            var result = await _employmentRepository.DeleteAsync(existingEntity);
            if (!result)
            {
                return new ResponseResult<bool> { IsSuccess = false, Message = "Failed to delete employment from database." };
            }
            return new ResponseResult<bool> { IsSuccess = true, Data = true, Message = "Employment deleted successfully." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in DeleteEmploymentAsync: {ex.Message}");
            return new ResponseResult<bool> { IsSuccess = false, Message = "An error occurred while deleting the employment." };
        }
    }


}





