using Microsoft.AspNetCore.Mvc;

namespace ZWebUI.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View("~/Pages/Shared/_Layout.cshtml");
        }
        public IActionResult ShoppingCart()
        {
            return View("~/Pages/Shared/shopping_cart.cshtml");
        }


    }
}
