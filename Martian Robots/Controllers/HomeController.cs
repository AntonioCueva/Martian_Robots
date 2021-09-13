using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Martian_Robots.Models;
using Martian_Robots.Data;

namespace Martian_Robots.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository repository;

        public HomeController(ILogger<HomeController> logger,IRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var lastMove = repository.getLastMoveNotLost();
            string nameRobot = null;
            var isRobotPlaying = false;

            if (lastMove != null)
            {
                nameRobot = lastMove.User;
                isRobotPlaying = true;
            }
            
            return View(new GameViewModel()
            {
                settings = repository.GetSettings(),
                Movement = lastMove,
                isRobotPlaying = isRobotPlaying,
                NameRobot = nameRobot

            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
