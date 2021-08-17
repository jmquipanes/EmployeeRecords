using EmployeeRecords.API.Controllers;
using EmployeeRecords.Core.DTO;
using EmployeeRecords.Core.Interfaces.Services;
using EmployeeRecords.Core.Parameters;
using EmployeeRecords.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeRecords.Test
{
    using mockEmployeeService = Mock<IEmployeeService>;

    [TestFixture]
    public class EmployeeControllerTest
    {
        public static mockEmployeeService CreateMockEmployeeService() => new mockEmployeeService();

        public static IEnumerable<Employee> CreateEmployeeList(int count)
        {
            var employees = new List<Employee>();
            for (int index = 0; index < count; index++)
            {
                var employee = new Employee
                {
                    Id = index + 1,
                    FirstName = index.ToString(),
                    MiddleName = index.ToString(),
                    LastName = index.ToString()
                };
                employees.Add(employee);
            }
            return employees;
        }

        public static Employee CreateEmployee()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "fname",
                MiddleName = "mname",
                LastName = "lname"
            };
            return employee;
        }

        [SetUp]
        public void Setup()
        {            
        }

        [Test]
        public void EmployeesControllerInitializedWithNullParameter()
        {
            var exceptionThrown = false;
            try
            {
                var controller = new EmployeesController(null);                    
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);
        }

        [Test]
        public void EmployeesControllerInitializeWithNonNullParameter()
        {
            var exceptionThrown = false;
            try
            {                
                var controller = new EmployeesController(CreateMockEmployeeService().Object);                
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }
            Assert.AreEqual(false, exceptionThrown);
        }

        [Test]
        public void GetAllEmployeeRecords()
        {
            var employeeService = CreateMockEmployeeService();
            employeeService.Setup(e => e.GetEmployeeRecordsAsync()).ReturnsAsync(CreateEmployeeList(3));
            var controller = new EmployeesController(employeeService.Object);
            var result = controller.GetAllEmployeeRecords().Result;
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode);
            Assert.AreEqual(3, ((List<Employee>)((OkObjectResult)result).Value).Count);
        }

        [Test]
        public async Task GetEmployeeRecordByIdWithEmployeeIdEqualsZero()
        {
            var exceptionThrown = false;
            try
            {
                var employeeService = CreateMockEmployeeService();
                var controller = new EmployeesController(employeeService.Object);
                await controller.GetEmployeeRecordById(0);
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }
            Assert.AreEqual(true, exceptionThrown);
        }

        [Test]
        public void GetEmployeeRecordByIdWithEmployeeIdGreaterThanZero()
        {
            var employeeService = CreateMockEmployeeService();
            employeeService.Setup(e => e.GetEmployeeRecordByIdAsync(It.IsAny<int>())).ReturnsAsync(CreateEmployee());
            var controller = new EmployeesController(employeeService.Object);
            var result = controller.GetEmployeeRecordById(1).Result;
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode);
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<Employee>());
        }

        [Test]
        public void CreateEmployeeRecordWithNullParameter()
        {
            var exceptionThrown = false;
            try
            {
                var employeeService = CreateMockEmployeeService();
                employeeService.Setup(e => e.CreateEmployeeRecordAsync(It.IsAny<EmployeeDTO>())).ReturnsAsync("Success");
                var controller = new EmployeesController(employeeService.Object);
                var result = controller.CreateEmployeeRecord(null).Result;                
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }
            Assert.AreEqual(true, exceptionThrown);
        }

        [Test]
        public void CreateEmployeeRecordWithNonNullParameter()
        {
            var employeeService = CreateMockEmployeeService();
            employeeService.Setup(e => e.CreateEmployeeRecordAsync(It.IsAny<EmployeeDTO>())).ReturnsAsync("Success");
            var controller = new EmployeesController(employeeService.Object);
            var result = controller.CreateEmployeeRecord(new EmployeeRecordParameters()).Result;
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode);
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<string>());
            Assert.AreEqual("Success", ((OkObjectResult)result).Value.ToString());
        }

        [Test]
        public void UpdateEmployeeRecordWithNullParameter()
        {
            var exceptionThrown = false;
            try
            {
                var employeeService = CreateMockEmployeeService();
                employeeService.Setup(e => e.UpdateEmployeeRecordByIdAsync(It.IsAny<EmployeeDTO>())).ReturnsAsync("Success");
                var controller = new EmployeesController(employeeService.Object);
                var result = controller.UpdateEmployeeRecord(null).Result;
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }
            Assert.AreEqual(true, exceptionThrown);
        }

        [Test]
        public void UpdateEmployeeRecordWithNonNullParameter()
        {
            var employeeService = CreateMockEmployeeService();
            employeeService.Setup(e => e.UpdateEmployeeRecordByIdAsync(It.IsAny<EmployeeDTO>())).ReturnsAsync("Success");
            var controller = new EmployeesController(employeeService.Object);
            var result = controller.UpdateEmployeeRecord(new EmployeeRecordParameters() { Id = 1 }).Result;
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode);
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<string>());
            Assert.AreEqual("Success", ((OkObjectResult)result).Value.ToString());
        }

        [Test]
        public void UpdateEmployeeRecordWithEmployeeIdParameterEqualToZero()
        {
            var exceptionThrown = false;
            try
            {
                var employeeService = CreateMockEmployeeService();
                employeeService.Setup(e => e.UpdateEmployeeRecordByIdAsync(It.IsAny<EmployeeDTO>())).ReturnsAsync("Success");
                var controller = new EmployeesController(employeeService.Object);
                var result = controller.UpdateEmployeeRecord(new EmployeeRecordParameters() { Id = 0 }).Result;
            }
            catch (System.Exception)
            {
                exceptionThrown = true;
            }
            Assert.AreEqual(true, exceptionThrown);
        }
    }
}