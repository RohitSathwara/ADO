using ADO.Models;
using ADO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeApiController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] Employee emp)
        {
            _repo.Insert(emp);
            return Ok("Employee Inserted");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put([FromBody] Employee emp)
        {
            _repo.Update(emp);
            return Ok("Employee Updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok(new { success = true, message = "Employee Deleted" });
        }

    }
}
