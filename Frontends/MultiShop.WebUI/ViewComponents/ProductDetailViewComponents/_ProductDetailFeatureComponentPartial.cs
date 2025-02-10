using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailFeatureComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;

        public _ProductDetailFeatureComponentPartial(IProductService productService, ICommentService commentService)
        {
            _productService = productService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            // Ürün detayını getir
            var product = await _productService.GetByIdProductAsync(id);

            // Yorumları al ve sayısını/punu hesapla
            var comments = await _commentService.CommentListByProductId(id);
            int commentCount = comments.Count;
            double averageRating = comments.Any() ? comments.Average(c => c.Rating) : 0;

            // ViewBag ile değerleri gönder
            ViewBag.CommentCount = commentCount;
            ViewBag.AverageRating = averageRating;

            return View(product);
        }
    }
}
