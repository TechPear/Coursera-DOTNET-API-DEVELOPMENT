using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                var user = _service.GetById(id);
                return user == null
                    ? NotFound(new { message = "User not found" })
                    : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                user.Id = 0;

                var created = _service.Add(user);
                if (created == null)
                    return Conflict(new { message = "A user with this email already exists." });

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                user.Id = id;

                var success = _service.Update(id, user);
                if (!success)
                    return Conflict(new { message = "Email already in use or user not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _service.Delete(id);
                return success ? NoContent() : NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }
    }
}