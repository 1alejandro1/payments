using Microsoft.AspNetCore.Mvc;

namespace ServicePaymentsAPI.Controllers
{
    public class payments : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
