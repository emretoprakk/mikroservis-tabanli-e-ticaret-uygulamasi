using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class _OrderSummaryComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;
        private const decimal TaxRate = 0.10m; // %10 KDV
        private const decimal ShippingCost = 45m;

        public _OrderSummaryComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var basketTotal = await _basketService.GetBasket();

                if (basketTotal == null || basketTotal.BasketItems == null)
                {
                    ViewBag.SubTotal = 0m;
                    ViewBag.Tax = 0m;
                    ViewBag.ShippingCost = 0m;
                    ViewBag.Total = 0m;
                    return View(new List<BasketItemDto>());
                }

                // Her ürünün kendi miktarı ile fiyatını çarpıp topluyoruz
                decimal subTotal = basketTotal.BasketItems.Sum(x => x.Price * x.Quantity);
                ViewBag.SubTotal = subTotal;

                // KDV hesaplama
                ViewBag.Tax = subTotal * TaxRate;

                // Kargo ücreti
                ViewBag.ShippingCost = subTotal > 0 ? ShippingCost : 0m;

                // Genel Toplam
                ViewBag.Total = subTotal + ViewBag.Tax + ViewBag.ShippingCost;

                return View(basketTotal.BasketItems);
            }
            catch
            {
                ViewBag.SubTotal = 0m;
                ViewBag.Tax = 0m;
                ViewBag.ShippingCost = 0m;
                ViewBag.Total = 0m;
                return View(new List<BasketItemDto>());
            }
        }
    }
}
