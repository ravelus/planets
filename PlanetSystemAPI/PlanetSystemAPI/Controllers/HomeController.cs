using System.Web.Mvc;

using PlanetSystemAPI.Helpers;
using PlanetSystemAPI2.Models;

namespace PlanetSystemAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // in spanish to attach to the requirements given
        public JsonResult Clima(int dia)
        {
            var result = new WeatherInfo
            {
                Dia = dia,
                Clima = WeatherHelper.GetWeatherOfDay(dia)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
