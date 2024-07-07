using JWT_Implementation.Context;
using JWT_Implementation.Interfaces;
using JWT_Implementation.Models;

namespace JWT_Implementation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _jwtcontext;

        public EmployeeService(JwtContext jwtcontext)
        {
            this._jwtcontext = jwtcontext;
        }

        public Employee AddEmployee(Employee employee)
        {
            var emp = _jwtcontext.Employees.Add(employee);
            _jwtcontext.SaveChanges();
            return emp.Entity;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var emp = _jwtcontext.Employees.SingleOrDefault(x => x.Id == id);
                if (emp == null)
                {
                    throw new Exception("Employee not found");
                }
                else
                {
                    _jwtcontext.Employees.Remove(emp);
                    _jwtcontext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

        }

        public List<Employee> GetEmployeeDetails()
        {
            var employees = _jwtcontext.Employees.ToList();
            return employees;
        }

        public Employee GetEmployeeDetails(int id)
        {
            var emp = _jwtcontext.Employees.SingleOrDefault(x => x.Id == id);
            return emp;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var empUpdated = _jwtcontext.Employees.Update(employee);
            _jwtcontext.SaveChanges();
            return empUpdated.Entity;
        }

 
    }
}
    