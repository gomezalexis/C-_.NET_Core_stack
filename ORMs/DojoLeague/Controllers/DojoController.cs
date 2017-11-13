using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Models;
using DojoLeague.Factories;

namespace DojoLeague.Controllers
{
    public class DojoController : Controller
    {
        private readonly DojoFactory dojoFactory;
        public DojoController(){
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("/newdojo")]
        public IActionResult NewDojo(){
            ViewBag.allDojos = dojoFactory.GetAllDojos();
            return View();
        }

        [HttpPost]
        [Route("/adddojo")]
        public IActionResult AddDojo(Dojo dojo){
            if(ModelState.IsValid){
                Console.WriteLine("Adding a Dojo");
                dojoFactory.AddNewDojo(dojo);
                return RedirectToAction("NewDojo");
            }
            ViewBag.allDojos = dojoFactory.GetAllDojos();
            return View("NewDojo", dojo);
        }
    }
}