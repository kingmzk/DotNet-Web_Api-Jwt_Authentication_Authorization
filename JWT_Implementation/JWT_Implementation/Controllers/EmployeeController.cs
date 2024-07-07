using JWT_Implementation.Interfaces;
using JWT_Implementation.Models;
using JWT_Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> Get()
        {
            return _employeeService.GetEmployeeDetails();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            var emp = _employeeService.GetEmployeeDetails(id);
            return emp;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            var emp = _employeeService.AddEmployee(employee);
            return emp;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public Employee Put(int id, [FromBody] Employee emp)
        {
            var employee = _employeeService.UpdateEmployee(emp);
            return employee;
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
