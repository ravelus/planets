using System.Web.Mvc;

using PlanetSystemAPI.Helpers;

namespace PlanetSystemAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // in spanish to attach to the requirements given
        public string Clima(int dia)
        {
            return WeatherHelper.GetWeatherOfDay(dia);
        }
    }
}
