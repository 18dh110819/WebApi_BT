using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("users/all")]
        public async Task<ActionResult> GetUsers()
        {
            List<User> lst = await _context.Users.Skip(0).Where(u => !u.Deleted).ToListAsync();
            var res = new ResponseType<List<User>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = lst,
                Message = "Users retrieved successfully",
                Timestamp = DateTime.UtcNow
            };
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsers(int pageIndx = 1, int size = 10)
        {
            if (pageIndx < 1 || size < 1)
            {
                return BadRequest(new ResponseType<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null,
                    Message = "Invalid page index or size",
                    Timestamp = DateTime.UtcNow
                });
            }

            int skip = (pageIndx - 1) * size;
            List<User> lst = await _context.Users.Skip(skip).Take(size).Where(u => !u.Deleted).ToListAsync();
            var res = new ResponseType<List<User>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = lst,
                Message = "Users retrieved successfully",
                Timestamp = DateTime.UtcNow
            };
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("users-ordered")]
        public async Task<ActionResult> GetUsersOrdered([FromQuery] int sortBy)
        {
            IEnumerable<User> lst = _context.Users.Where(u => !u.Deleted).OrderBy(u => u.Name);

            // 1: Id, 2: Name, 3: CreatedAt
            switch (sortBy)
            {
                case 1:
                    lst = lst.OrderBy(u => u.Id);
                    break;
                case 2:
                    lst = lst.OrderBy(u => u.Name);
                    break;
                case 3:
                    lst = lst.OrderBy(u => u.CreatedAt);
                    break;
                default:
                    return BadRequest(new ResponseType<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Invalid sorting choice",
                        Timestamp = DateTime.UtcNow
                    });
            }

            var res = new ResponseType<List<User>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [.. lst],
                Message = "Users retrieved successfully",
                Timestamp = DateTime.UtcNow
            };
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("users/search")]
        public async Task<ActionResult> SearchUsers([FromQuery] string? searchTerm)
        {
            var keyword = !string.IsNullOrEmpty(searchTerm) ? HelperFunction.StringToSlug(searchTerm) : string.Empty;
            var lst = _context.Users.Where(u => !u.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                lst = lst.Where(u => u.Alias.Contains(keyword));
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseType<List<User>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = [.. lst],
                Message = "Users retrieved successfully",
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult> GetUser([FromRoute] int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id && !u.Deleted);
            if (user == null)
            {
                return NotFound(new ResponseType<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found",
                    Timestamp = DateTime.UtcNow
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseType<User>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = user,
                Message = "User retrieved successfully",
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpPost("users/create")]
        public async Task<ActionResult> CreateUser([FromBody] UserInsertDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Alias = HelperFunction.StringToSlug(dto.Name),
                Email = dto.Email,
                Age = dto.Age
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, new ResponseType<User>()
            {
                StatusCode = StatusCodes.Status201Created,
                Data = user,
                Message = "User created successfully",
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpPut("users/update/{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id && !u.Deleted);
            if (user == null)
            {
                return NotFound(new ResponseType<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found",
                    Timestamp = DateTime.UtcNow
                });
            }

            user.Name = dto.Name;
            user.Alias = HelperFunction.StringToSlug(dto.Name);
            user.Email = dto.Email;
            user.Age = dto.Age;
            user.Description = dto.Description;

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new ResponseType<User>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = user,
                Message = "User updated successfully",
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpDelete("users/delete/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id && !u.Deleted);
            if (user == null)
            {
                return NotFound(new ResponseType<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found",
                    Timestamp = DateTime.UtcNow
                });
            }

            user.Deleted = true;
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new ResponseType<string>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = null,
                Message = "User deleted successfully",
                Timestamp = DateTime.UtcNow
            });
        }


    }
}