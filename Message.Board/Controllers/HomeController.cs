using Microsoft.AspNetCore.Mvc;

namespace Message.Board.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
