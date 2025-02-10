using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.PaymentServices;
using System.Transactions;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IOrderOrderingService _orderOrderingService;

        public PaymentController(
            IPaymentService paymentService,
            IBasketService basketService,
            IUserService userService,
            IOrderOrderingService orderOrderingService)
        {
            _paymentService = paymentService;
            _basketService = basketService;
            _userService = userService;
            _orderOrderingService = orderOrderingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Ödeme Ekranı";
            ViewBag.directory3 = "Kartla Ödeme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompletePayment()
        {
            try
            {
                // Kullanıcı bilgilerini al
                var user = await _userService.GetUserInfo();
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Kullanıcı bilgileri alınamadı.";
                    return RedirectToAction("Index");
                }

                // Sepet bilgilerini al
                var basket = await _basketService.GetBasket();
                if (basket == null || !basket.BasketItems.Any())
                {
                    TempData["ErrorMessage"] = "Sepetiniz boş. Ödeme işlemi gerçekleştirilemiyor.";
                    return RedirectToAction("Index", "ShoppingCart");
                }

                // Sipariş DTO'sunu oluştur
                var createOrderDto = new CreateOrderingDto
                {
                    UserId = user.Id,
                    TotalPrice = basket.TotalPrice,
                    OrderDate = DateTime.UtcNow,
                    OrderDetails = basket.BasketItems.Select(item => new CreateOrderDetailDto
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ProductPrice = item.Price,
                        ProductAmount = item.Quantity,
                        ProductTotalPrice = item.Price * item.Quantity
                    }).ToList()
                };

                // Ödeme işlemini başlat
                var result = await _paymentService.CompletePaymentAsync(createOrderDto);

                if (result)
                {
                    TempData["SuccessMessage"] = "Ödeme işlemi başarıyla tamamlandı!";
                    return RedirectToAction("Success");
                }

                TempData["ErrorMessage"] = "Ödeme işlemi sırasında bir hata oluştu.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Beklenmeyen bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Success()
        {
            var user = await _userService.GetUserInfo();
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri alınamadı.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            // Kullanıcının son siparişini al
            var orders = await _orderOrderingService.GetOrderingByUserId(user.Id);
            var lastOrder = orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();

            if (lastOrder != null)
            {
                ViewBag.OrderingId = lastOrder.OrderingId;
            }
            else
            {
                TempData["ErrorMessage"] = "Sipariş bilgileri alınamadı.";
                return RedirectToAction("Index", "ShoppingCart");
            }

            // ViewBag ile başlıkları ayarla
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sipariş";
            ViewBag.directory3 = "Sipariş Tamamlandı";

            return View();
        }
    }


}


