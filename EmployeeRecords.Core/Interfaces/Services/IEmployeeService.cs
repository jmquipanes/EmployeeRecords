using EmployeeRecords.Core.DTO;
using EmployeeRecords.EF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRecords.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<string> CreateEmployeeRecordAsync(EmployeeDTO employee);
        Task<IEnumerable<Employee>> GetEmployeeRecordsAsync();
        Task<Employee> GetEmployeeRecordByIdAsync(int employeeId);
        Task<string> UpdateEmployeeRecordByIdAsync(EmployeeDTO employee);
        Task<string> DeleteEmployeeRecord(int employeeId);
    }
}
