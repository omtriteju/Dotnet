using Groko.core.Services;
using Groko.Core.Entities;
using Groko.Core.QueryModels;
using Microsoft.AspNetCore.Mvc;

namespace Groko.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogBytesController : ControllerBase
    {
        private readonly IBlogBytesService _blogbyteService;

        public BlogBytesController(IBlogBytesService blogbyteService)
        {
            _blogbyteService = blogbyteService;
        }

        [HttpPost("GetAllBlogBytes")]
        public async Task<ActionResult<IEnumerable<BlogBytes>>> GetAllBlogBytes([FromBody] BlogBytesFilter blogsfilter)
        {
            var blogs = await _blogbyteService.GetBlogBytesAsync(blogsfilter);
            return Ok(blogs);
        }


        [HttpPost("CreateBlog")]
        public async Task<ActionResult> CreateBlog([FromBody] BlogBytes blogBytes)
        {
            await _blogbyteService.CreateBlogByteAsync(blogBytes);
            return Ok();
        }

        [HttpPut("UpdateBlog")]
        public async Task<ActionResult> UpdateBlog([FromBody] BlogBytes blogBytes)
        {
            await _blogbyteService.UpdateBlogByteAsync(blogBytes);
            return Ok();
        }

        [HttpDelete("DeleteBlog/{BlogId}")]
        public async Task<ActionResult> DeleteBlog(Guid BlogId)
        {
            await _blogbyteService.DeleteBlogByteAsync(BlogId);
            return Ok();
        }


    }
}
