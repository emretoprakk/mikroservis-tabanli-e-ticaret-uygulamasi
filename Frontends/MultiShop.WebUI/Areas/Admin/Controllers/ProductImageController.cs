using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _ProductImageService;
        public ProductImageController(IProductImageService ProductImageService)
        {
            _ProductImageService = ProductImageService;
        }

        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            ProductImageViewBagList();
            var values = await _ProductImageService.GetAllProductImageAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateProductImage/{productId}")]
        public IActionResult CreateProductImage(string productId)
        {
            ProductImageViewBagList();
            var model = new CreateProductImageDto
            {
                ProductId = productId
            };
            return View(model);
        }


        [HttpPost]
        [Route("CreateProductImage/{productId}")]
        public async Task<IActionResult> CreateProductImage(string productId, CreateProductImageDto createProductImageDto)
        {
            createProductImageDto.ProductId = productId;
            await _ProductImageService.CreateProductImageAsync(createProductImageDto);

            // Ürüne geri yönlendirme
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [Route("DeleteProductImage/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await _ProductImageService.DeleteProductImageAsync(id);
            return RedirectToAction("Index", "ProductImage", new { area = "Admin" });
        }

        [Route("UpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            ProductImageViewBagList();
            var values = await _ProductImageService.GetByIdProductImageAsync(id);
            return View(values);
        }

        [HttpPost]
        [Route("UpdateProductImage")]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            await _ProductImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        [Route("ProductImageDetail/{productId}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string productId)
        {
            ProductImageViewBagList();
            var values = await _ProductImageService.GetByProductIdProductImageAsync(productId);
            if (values == null)
            {
                return NotFound("Ürün için görsel bulunamadı.");
            }
            return View(values);
        }



        void ProductImageViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Güncelleme Sayfası";
            ViewBag.v0 = "Ürün Görseli İşlemleri";
        }
    }
}

