using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BadRequest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ServerError()
        {
            return View();
        }
    }
}