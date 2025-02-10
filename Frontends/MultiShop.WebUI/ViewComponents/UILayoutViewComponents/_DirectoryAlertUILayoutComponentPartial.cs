using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _DirectoryAlertUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(bool IsLoginPage = false, bool IsRegisterPage = false)
        {
            ViewData["IsLoginPage"] = IsLoginPage;
            ViewData["IsRegisterPage"] = IsRegisterPage;
            return View();
        }
    }
}
