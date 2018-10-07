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
            return JsonifyResponse(dia, WeatherHelper.GetWeatherOfDay(dia));
        }

        string JsonifyResponse(int day, string weather)
        {
            //There's no point in adding REAL Json support for such a simple response
            return $"{{\"dia\":{day}, \"clima\":\"{weather}\"}}";
        }
    }
}
