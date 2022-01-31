using System.Diagnostics;
using Imi.Project.Vue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imi.Project.Vue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/games")]
        public IActionResult ShowGames()
        {
            return View("Games");
        }

        [Route("/locations")]
        public IActionResult ShowLocations()
        {
            return View("Locations");
        }

        [Route("/rackets")]
        public IActionResult ShowRackets()
        {
            return View("Rackets");
        }

        [Route("/shuttles")]
        public IActionResult ShowShuttles()
        {
            return View("Shuttles");
        }

        [Route("/pokemon")]
        public IActionResult ShowPokemon()
        {
            return View("Pokemon");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}