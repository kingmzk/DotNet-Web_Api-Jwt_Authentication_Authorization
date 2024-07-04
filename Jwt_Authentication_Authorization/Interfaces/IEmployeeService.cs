using Jwt_Authentication_Authorization.Models;


namespace Jwt_Authentication_Authorization.Interfaces
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeDetails();

        public Employee AddEmployee(Employee employee);

    }
}
