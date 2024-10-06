using JwtAuthenticationDemoAspNetCore.Models;

namespace JwtAuthenticationDemoAspNetCore.Interfaces
{
    public interface IEmployeeService
    {
        //CRUD methods related to Employee
        public List<Employee> GetEmployeeDetails();
        public Employee GetEmployeeDetailsById(int id);
        public Employee AddEmployee(Employee employee);
        public Employee UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int id);
    }
}
