using JwtAuthenticationDemoAspNetCore.Interfaces;
using JwtAuthenticationDemoAspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthenticationDemoAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> GetAllEmployee()
        {
            var employees = _employeeService.GetEmployeeDetails();
            return employees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee GetEmployeeById(int id)
        {
            var emp = _employeeService.GetEmployeeDetailsById(id);
            return emp;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public Employee CreateEmployee([FromBody] Employee employee)
        {
            var emp = _employeeService.AddEmployee(employee);
            return emp;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public Employee UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var emp = _employeeService.UpdateEmployee(employee);
            return emp;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var isDeleted = _employeeService.DeleteEmployee(id);
            return isDeleted;
        }
    }
}
