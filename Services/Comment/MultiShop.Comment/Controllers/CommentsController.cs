using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;
using System.Xml.Linq;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentsController(CommentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.UserComments.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(UserComment userComment)
        {
            _context.UserComments.Add(userComment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            _context.UserComments.Update(userComment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla güncellendi.");
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var value = _context.UserComments.Find(id);
            _context.UserComments.Remove(value);
            _context.SaveChanges();
            return Ok("Yorum başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _context.UserComments.Find(id);
            return Ok(value);
        }

        [HttpGet("CommentListByProductId/{id}")]
        public IActionResult CommentListByProductId(string id)
        {
            var value = _context.UserComments
                                .Where(x => x.ProductId == id)
                                .OrderByDescending(x => x.CreatedDate) // En yeni yorumlar en üstte
                                .ToList();
            return Ok(value);
        }

        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount()
        {
            int value = _context.UserComments.Where(x=> x.Status == true).Count();
            return Ok(value);
        }

        [HttpGet("GetPassiveCommentCount")]
        public IActionResult GetPassiveCommentCount()
        {
            int value = _context.UserComments.Where(x => x.Status == false).Count();
            return Ok(value);
        }

        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount()
        {
            int value = _context.UserComments.Count();
            return Ok(value);
        }
    }
}
