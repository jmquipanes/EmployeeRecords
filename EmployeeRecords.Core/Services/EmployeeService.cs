using EmployeeRecords.Core.DTO;
using EmployeeRecords.Core.Interfaces.Services;
using EmployeeRecords.EF.Data;
using EmployeeRecords.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRecords.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRecordsDbContext _dbContext;

        public EmployeeService(EmployeeRecordsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<string> CreateEmployeeRecordAsync(EmployeeDTO employee)
        {
            var employeeRecord = new Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddelName,
                LastName = employee.LastName
            };
            await this._dbContext.AddAsync(employeeRecord);
            await this._dbContext.SaveChangesAsync();
            return "Employee Record Created Successfully";
        }

        public async Task<string> DeleteEmployeeRecord(int employeeId)
        {
            var employeeRecord = this._dbContext.Find<Employee>(employeeId);
            this._dbContext.Remove(employeeRecord);
            await this._dbContext.SaveChangesAsync();
            return "Employee Record Deleted Successfully";
        }

        public async Task<Employee> GetEmployeeRecordByIdAsync(int employeeId)
        {
            var employee = await this._dbContext.Employee.FirstOrDefaultAsync(e => e.Id == employeeId);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeRecordsAsync()
        {
            var employees = await this._dbContext.Employee.ToListAsync();
            return employees;
        }

        public async Task<string> UpdateEmployeeRecordByIdAsync(EmployeeDTO employee)
        {
            var employeeRecord = new Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddelName,
                LastName = employee.LastName
            };
            this._dbContext.Update(employeeRecord);            
            await this._dbContext.SaveChangesAsync();
            return "Employee Record Updated Successfully";
        }
    }
}
