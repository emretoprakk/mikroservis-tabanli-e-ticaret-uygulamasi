using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    namespace MultiShop.WebUI.Controllers
    {
        public class ShoppingCartController : Controller
        {
            private readonly IProductService _productService;
            private readonly IBasketService _basketService;

            public ShoppingCartController(IProductService productService, IBasketService basketService)
            {
                _productService = productService;
                _basketService = basketService;
            }

            public async Task<IActionResult> Index(string code, int discountRate = 0)
            {
                ViewBag.directory1 = "Ana Sayfa";
                ViewBag.directory2 = "Ürünler";
                ViewBag.directory3 = "Sepetim";

                var basketValues = await _basketService.GetBasket();
                var subtotal = basketValues.TotalPrice;
                var tax = subtotal * 0.10m; // %10 KDV

                // Apply discount if valid code is provided
                decimal discountAmount = 0;
                if (!string.IsNullOrEmpty(code) && discountRate > 0)
                {
                    discountAmount = (subtotal * discountRate) / 100;
                }

                var subtotalWithDiscount = subtotal - discountAmount;
                var totalWithTax = subtotalWithDiscount + tax;

                // Set ViewBag values
                ViewBag.Code = code ?? "";
                ViewBag.DiscountRate = discountRate;
                ViewBag.Total = subtotal;
                ViewBag.Tax = tax;
                ViewBag.TotalPriceWithTax = totalWithTax;
                ViewBag.TotalNewPriceWithDiscount = totalWithTax;

                return View();
            }


            public async Task<IActionResult> AddBasketItem(string id, int quantity = 1)
            {
                var product = await _productService.GetByIdProductAsync(id);

                if (product == null)
                {
                    return NotFound(); // Ürün bulunamadıysa 404 döndür
                }

                var item = new BasketItemDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.ProductPrice,
                    Quantity = quantity, // Gelen miktarı burada kullanıyoruz
                    ProductImageUrl = product.ProductImageUrl
                };

                await _basketService.AddBasketItem(item);

                return RedirectToAction("Index", "ShoppingCart");
            }

            public async Task<IActionResult> RemoveBasketItem(string id)
            {
                await _basketService.RemoveBasketItem(id);
                return RedirectToAction("Index");
            }

            [HttpPost]
            public async Task<IActionResult> UpdateBasketQuantity([FromBody] BasketItemDto basketItem)
            {
                var basket = await _basketService.GetBasket();
                if (basket == null || basket.BasketItems == null)
                {
                    return Json(new { success = false, message = "Sepet bulunamadı." });
                }

                var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
                if (item != null)
                {
                    item.Quantity = basketItem.Quantity;
                    await _basketService.SaveBasket(basket);

                    var subtotal = basket.BasketItems.Sum(x => x.Price * x.Quantity);
                    var tax = subtotal * 0.10m; // %10 KDV
                    var total = subtotal + tax;

                    return Json(new
                    {
                        success = true,
                        subtotal,
                        tax,
                        total
                    });
                }

                return Json(new { success = false, message = "Ürün bulunamadı." });
            }

            [HttpGet]
            public async Task<IActionResult> GetCartSummary()
            {
                var basket = await _basketService.GetBasket();
                var subtotal = basket.TotalPrice;
                var tax = subtotal * 0.1m; // 10% tax

                return Json(new
                {
                    subtotal,
                    tax,
                    total = subtotal + tax
                });
            }

            [HttpPost]
            public async Task<IActionResult> ProceedToOrder()
            {
                var basket = await _basketService.GetBasket();

                if (basket == null || !basket.BasketItems.Any())
                {
                    TempData["ErrorMessage"] = "Sepetiniz boş. Lütfen ürün ekleyin.";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Order");
            }
        }
    }
}






