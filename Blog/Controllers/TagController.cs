using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class TagController : ControllerBase
    {
        private readonly BlogDataContext _context;

        public TagController(BlogDataContext context)
        {
            _context = context;
        }


        [HttpGet("v1/tags")]
        public async Task<IActionResult> GetAsync()
        {
            var tags = await _context
                .Tags.AsNoTracking()
                .ToListAsync();
            if (tags == null)
                return NotFound(new ResultViewModel<List<Tag>>("não foi possível listar as tags "));
            return Ok(new ResultViewModel<List<Tag>>(tags));
        }

        [HttpGet("v1/tags{id}")]
        public IActionResult GetByIdAsync(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (tag == null)
                return NotFound(new ResultViewModel<Tag>("não foi possível localizar a tag "));
            return Ok(new ResultViewModel<Tag>(tag));
        }

        [HttpPost("v1/tags")]
        public async Task<IActionResult> PostAsync([FromBody] EditorTagViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Tag>(ModelState.GetErros()));

                var tag = new Tag
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug,
                    Posts = null
                };

                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return Ok(new ResultViewModel<Tag>(tag));
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new ResultViewModel<Tag>($"Ecorreu uma exceção ao cadastrar a tag, erro: {ex.Message}"));
            }

        }

        [HttpPut("v1/tags")]
        public async Task<IActionResult> PutAsync([FromBody] EditorTagViewModel model, int id)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Tag>(ModelState.GetErros()));

                var tag = _context.Tags.FirstOrDefault(x => x.Id == id);

                if (tag == null)
                    return NotFound(new ResultViewModel<Tag>("não foi possível localizar a tag "));

                tag.Name = model.Name;
                tag.Slug = model.Slug;

                _context.Tags.Update(tag);
                await _context.SaveChangesAsync();
                return Ok(new ResultViewModel<Tag>(tag));
            }
            catch (System.Exception)
            {
                return BadRequest(new ResultViewModel<Tag>($"Ecorreu uma exceção ao cadastrar a tag "));
            }

        }

        [HttpDelete("v1/tags{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (tag == null)
                return NotFound(new ResultViewModel<Tag>("não foi possível localizar a tag "));

            _context.Tags.Remove(tag);
            _context.SaveChangesAsync();

            return Ok(new ResultViewModel<Tag>(tag));
        }
    }
}