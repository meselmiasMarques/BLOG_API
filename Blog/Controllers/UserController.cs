using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BlogDataContext _context;

        public UserController(BlogDataContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet("v1/users")]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return StatusCode(401, new ResultViewModel<User>("Erro ao consultar Usuarios"));
            }

            return StatusCode(200, new List<User>(users));
        }

        // GET api/<UserController>/5
        [HttpGet("v1/users/{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound(new ResultViewModel<User>("erro ao encontrar usuario"));
            }

            return Ok(new ResultViewModel<User>(user));
        }

        // POST api/<UserController>
        [HttpPost("v1/users")]
        public async Task<IActionResult> Post([FromBody] EditorUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErros()));

            var user = new User
            {
                Id = 0,
                Name = model.Name,
                Email = model.Email,
                Slug = model.Slug,
                Bio = model.Bio,
                Image = model.Image,
                PasswordHash = model.PasswordHash,
                Posts = null,
                Roles = null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created($"v1/categories/{user.Id}", new ResultViewModel<User>(user));

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditorUserViewModel model)
        {
            var user = _context
                .Users.AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new ResultViewModel<User>("Usuario não encontrado"));
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Slug = model.Slug;
            user.Bio = model.Bio;
            user.Image = model.Image;
            user.PasswordHash = model.PasswordHash;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewModel<User>(user));
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _context
                .Users.AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new ResultViewModel<EditorUserViewModel>("Usuario nao encontrado"));
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));
        }
    }
}
