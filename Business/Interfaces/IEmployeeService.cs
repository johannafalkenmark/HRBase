using Business.Models;
using Business.Models.ResponseModels;

namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseResult<Employee>> CreateEmployeeAsync(EmployeeRegistrationForm employeeRegistrationForm);
        Task<ResponseResult<bool>> DeleteEmployeeAsync(int id);
        Task<ResponseResult<IEnumerable<Employee>>> GetAllEmployeesAsync();
        Task<ResponseResult<Employee>> GetEmployeeByIdAsync(int id);
        Task<ResponseResult<bool>> UpdateEmployeeAsync(int id, EmployeeRegistrationForm employeeRegistrationForm);
    }
}