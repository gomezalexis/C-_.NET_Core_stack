using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LostInTheWoods.Models;
using LostInTheWoods.Factories;

namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController(){
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.allTrails = trailFactory.GetAllTrails();
            return View();
        }

        [HttpPost]
        [Route("/addtrail")]
        public IActionResult AddTrail(Trail trail){
            if(ModelState.IsValid){
                System.Console.WriteLine("Adding a trail. It was valid btw");
                trailFactory.AddNewTrail(trail);
                return Redirect("/");
            }

            return View("NewTrail", trail);
        }

        [HttpGet]
        [Route("/newtrail")]
        public IActionResult NewTrail(){
            return View();
        }

        [HttpGet]
        [Route("/show/{Id}")]
        public IActionResult ShowTrail(int Id){
            ViewBag.theTrail = trailFactory.FindById(Id);
            return View();
        }

    }
}
