using Jwt_Authentication_Authorization.Context;
using Jwt_Authentication_Authorization.Interfaces;
using Jwt_Authentication_Authorization.Models;

namespace Jwt_Authentication_Authorization.Services
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly JwtContext _context;
        public EmployeeService(JwtContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
           var emp = _context.Employees.Add(employee);
           _context.SaveChanges();
           return emp.Entity;
        }

        public List<Employee> GetEmployeeDetails()
        {
            var employees =  _context.Employees.ToList();
            return employees;
        }
    }
}
 