using BadgeCRUD.Model;
using BadgeCRUD.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _employee.GetEmployees();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees([FromBody] Employee employ)
        {
            try
            {
                await _employee.AddEmployee(employ);
                return Ok("Employee Added");
            }catch(Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployees(int id, [FromBody] Employee employ)
        {
            try
            {
                await _employee.UpdateEmployee(id, employ);
                return Ok("Employee Details Updated");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            try
            {
                await _employee.DeleteEmployee(id);
                return Ok("Employee Deleted");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
