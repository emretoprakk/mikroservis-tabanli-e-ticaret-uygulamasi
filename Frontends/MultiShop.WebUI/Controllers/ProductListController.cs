using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICommentService _commentService;
        public ProductListController(IHttpClientFactory httpClientFactory, ICommentService commentService)
        {
            _httpClientFactory = httpClientFactory;
            _commentService = commentService;
        }
        public IActionResult Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.i = id;
            return View();
        }
        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory3 = "Ürün Detayları";
            ViewBag.x = id;
            return View();
        }
        [HttpGet]
        public PartialViewResult AddComment()
        {
            var model = new CreateCommentDto();
            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto createCommentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false });
                }
                await _commentService.CreateCommentAsync(createCommentDto);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductComments(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("ID boş olamaz");
                }
                var comments = await _commentService.CommentListByProductId(id);
                return PartialView("_ProductDetailReviewComponentPartial", comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
