using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _NavbarUILayoutComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Kullanıcının oturum durumunu kontrol et
            bool isAuthenticated = HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated;

            // Giriş durumunu ViewData ile taşı
            ViewData["IsAuthenticated"] = isAuthenticated;

            // Kategorileri al
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }
    }
}
