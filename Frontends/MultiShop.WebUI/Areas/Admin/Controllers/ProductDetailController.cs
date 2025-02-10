using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _ProductDetailService;
        public ProductDetailController(IProductDetailService ProductDetailService)
        {
            _ProductDetailService = ProductDetailService;
        }

        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            ProductDetailViewBagList();
            var values = await _ProductDetailService.GetAllProductDetailAsync();
            return View(values);
        }

        [HttpGet]
        [Route("CreateProductDetail")]
        public IActionResult CreateProductDetail()
        {
            ProductDetailViewBagList();
            return View();
        }

        [HttpPost]
        [Route("CreateProductDetail")]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            if (ModelState.IsValid)
            {
                await _ProductDetailService.CreateProductDetailAsync(createProductDetailDto);
                return RedirectToAction("ProductDetailList", "ProductDetail", new { area = "Admin" });
            }

            return View(createProductDetailDto);
        }

        [Route("DeleteProductDetail/{id}")]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _ProductDetailService.DeleteProductDetailAsync(id);
            return RedirectToAction("ProductDetailList", "ProductDetail", new { area = "Admin" });
        }

        [Route("UpdateProductDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ProductDetailViewBagList();
            var values = await _ProductDetailService.GetByIdProductDetailAsync(id);
            return View(values);
        }

        [HttpPost]
        [Route("UpdateProductDetail/{id}")]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _ProductDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return RedirectToAction("ProductDetailList");
        }

        [Route("ProductDetailList")]
        public async Task<ActionResult> ProductDetailList()
        {
            ProductDetailViewBagList();
            var values = await _ProductDetailService.GetAllProductDetailAsync();
            return View(values);
        }

        void ProductDetailViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Açıklama ve Bilgi Güncelleme Sayfası";
            ViewBag.v0 = "Ürün İşlemleri";
        }
    }
}
