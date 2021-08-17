using EmployeeRecords.Core.DTO;
using EmployeeRecords.Core.Interfaces.Services;
using EmployeeRecords.Core.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeRecords.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeRecords()
        {
            var employeeRecords = await this._employeeService.GetEmployeeRecordsAsync();
            return Ok(employeeRecords);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeRecordById(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException($"Argument {nameof(employeeId)} cannot be less than or equal to zero");
            }

            var employeeRecord = await this._employeeService.GetEmployeeRecordByIdAsync(employeeId);
            return Ok(employeeRecord);
        }

        // POST api/<EmployeesController>
        [HttpPost("CreateEmployeeRecord")]
        public async Task<IActionResult> CreateEmployeeRecord([FromBody] EmployeeRecordParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var employeeParameters = this.MapParameterToDTO(parameters);
            var message = await this._employeeService.CreateEmployeeRecordAsync(employeeParameters);
            return Ok(message);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("UpdateEmployeeRecord")]
        public async Task<IActionResult> UpdateEmployeeRecord([FromBody] EmployeeRecordParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Id <= 0)
            {
                throw new ArgumentException($"Argument {nameof(parameters.Id)} cannot be less than or equal to zero");
            }

            var employeeParameters = this.MapParameterToDTO(parameters);
            var message = await this._employeeService.UpdateEmployeeRecordByIdAsync(employeeParameters);
            return Ok(message);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("DeleteEmployeeRecord/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeRecord(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException($"Argument {nameof(employeeId)} cannot be less than or equal to zero");
            }

            var message = await this._employeeService.DeleteEmployeeRecord(employeeId);
            return Ok(message);
        }

        private EmployeeDTO MapParameterToDTO(EmployeeRecordParameters parameters)
        {
            return new EmployeeDTO()
            {
                Id = parameters.Id,
                FirstName = parameters.FirstName,
                MiddelName = parameters.MiddelName,
                LastName = parameters.LastName
            };
        }
    }
}
