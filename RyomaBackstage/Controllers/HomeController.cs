using System.Web.Mvc;

namespace RyomaBackstage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}