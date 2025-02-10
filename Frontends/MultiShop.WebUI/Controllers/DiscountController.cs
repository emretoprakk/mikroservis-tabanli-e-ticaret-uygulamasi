using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            int discountRate = 0;
            if (!string.IsNullOrEmpty(code))
            {
                discountRate = await _discountService.GetDiscountCouponCountRate(code);
            }

            return Json(new
            {
                success = discountRate > 0,
                discountRate = discountRate
            });
        }

        [HttpPost]
        public IActionResult RemoveDiscountCoupon()
        {
            return Json(new
            {
                success = true,
                discountRate = 0
            });
        }
    }
}
