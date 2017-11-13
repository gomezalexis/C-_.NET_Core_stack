using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Models;
using DojoLeague.Factories;

namespace DojoLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;
        public HomeController(){
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.allNinjas = ninjaFactory.GetAllNinjasWithHomeDojos();
            ViewBag.allDojos = dojoFactory.GetAllDojos();
            return View();
        }

        [HttpPost]
        [Route("/addninja")]
        public IActionResult AddNinja(Ninja ninja){
            if(ModelState.IsValid){
                Console.WriteLine("Adding a ninja.");
                ninjaFactory.AddNewNinja(ninja);
                return Redirect("/");

            }
            ViewBag.allNinjas = ninjaFactory.GetAllNinjasWithHomeDojos();
            ViewBag.allDojos = dojoFactory.GetAllDojos();
            System.Console.WriteLine("---------------------------");
            System.Console.WriteLine("Didn't pass validation");
            return View("Index", ninja);
        }

        [HttpGet]
        [Route("/ninja/{id}")]
        public IActionResult ShowNinja(int id){
            Ninja theNinja = ninjaFactory.FindById(id);
            System.Console.WriteLine($"The ninja Id is {theNinja.dojo_id}");
            ViewBag.theNinja = theNinja;
            ViewBag.theDojo = dojoFactory.FindById(theNinja.dojo_id);
            return View();
        }

        [HttpGet]
        [Route("/dojo/{id}")]
        public IActionResult ShowDojo(int id){
            Dojo theDojo = dojoFactory.FindById(id);
            ViewBag.theDojo = theDojo;
            ViewBag.allHouseNinjas = ninjaFactory.GetAllFromDojo(theDojo.id);
            ViewBag.allRogues = ninjaFactory.GetRogues();
            return View();
        }

        [HttpGet]
        [Route("/{ninjaId}/banish/{dojoId}")]
        public IActionResult BanishNinja(int ninjaId, int dojoId){
            ninjaFactory.BanishNinja(ninjaId);
            return Redirect($"/dojo/{dojoId}");
        }

        [HttpGet]
        [Route("/{ninjaId}/recruit/{dojoId}")]
        public IActionResult RecruitNinja(int ninjaId, int dojoId){
            ninjaFactory.RecruitNinja(ninjaId, dojoId);
            return Redirect($"/dojo/{dojoId}");
        }
    }
}
