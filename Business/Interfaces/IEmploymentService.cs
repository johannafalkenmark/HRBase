using Business.Models;
using Business.Models.ResponseModels;

namespace Business.Interfaces
{
    public interface IEmploymentService
    {
        Task<ResponseResult<Employment>> CreateEmploymentAsync(EmploymentRegistrationForm employmentForm);
        Task<ResponseResult<bool>> DeleteEmploymentAsync(int id);
        Task<ResponseResult<IEnumerable<Employment>>> GetAllEmploymentsAsync();
        Task<ResponseResult<Employment>> GetEmploymentByIdAsync(int id);
        Task<ResponseResult<bool>> UpdateEmploymentAsync(int id, EmploymentRegistrationForm employmentForm);
    }
}