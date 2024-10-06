using JwtAuthenticationDemoAspNetCore.Context;
using JwtAuthenticationDemoAspNetCore.Interfaces;
using JwtAuthenticationDemoAspNetCore.Models;

namespace JwtAuthenticationDemoAspNetCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtDbContext _jwtContext;

        //inject the dependency
        public EmployeeService(JwtDbContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public Employee AddEmployee(Employee employee)
        {
            var emp = _jwtContext.Employees.Add(employee);

            _jwtContext.SaveChanges();

            return emp.Entity;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var emp = _jwtContext.Employees.SingleOrDefault(x => x.Id == id);
                if (emp == null)
                {
                    throw new Exception("User not found");
                }
                else
                {
                    _jwtContext.Employees.Remove(emp);
                    _jwtContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Employee> GetEmployeeDetails()
        {
            var employees = _jwtContext.Employees.ToList();
            return employees;
        }

        public Employee GetEmployeeDetailsById(int id)
        {
            var emp = _jwtContext.Employees.SingleOrDefault(x => x.Id == id);
            return emp;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var updatedEmployee = _jwtContext.Employees.Update(employee);
            _jwtContext.SaveChanges();

            return updatedEmployee.Entity;
        }
    }
}
