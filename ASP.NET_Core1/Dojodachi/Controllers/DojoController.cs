using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Dojodachi.Controllers{
    public class DojoController : Controller{
        [HttpGet]
        [Route("")]
        public IActionResult Index(){

            return View();
        }

        [HttpPost]
        [Route("setsession")]
        public IActionResult GettingSess(string TheName){

            HttpContext.Session.SetString("UserName", TheName);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            
            return RedirectToAction("Dojodachi");
        }

        [HttpGet]
        [Route("dojodachi")]
        public IActionResult Dojodachi(){
            int happiness = (int)HttpContext.Session.GetInt32("Happiness");
            int fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int energy = (int)HttpContext.Session.GetInt32("Energy");
            int meals = (int)HttpContext.Session.GetInt32("Meals");
            string name = HttpContext.Session.GetString("UserName");

            if(fullness <= 0 || happiness <= 0){
                ViewBag.over = true;
                ViewBag.message = $"Sorry you lost. Your fullness is {fullness} and your happiness is {happiness}.";
                return View();
            } else if(energy >= 100 && fullness >= 100 && happiness >= 100){
                ViewBag.over = true;
                ViewBag.message = "You won. Your energy, fullness and happiness are over 100.";
                return View();
            }else{

                ViewBag.name = name;
                ViewBag.happiness = happiness;
                ViewBag.fullness = fullness;
                ViewBag.energy = energy;
                ViewBag.meals = meals;
                ViewBag.message = TempData["Message"];

                return View();
            }
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed(){
            Random rand = new Random();
            var meals = HttpContext.Session.GetInt32("Meals");
            var fullness = HttpContext.Session.GetInt32("Fullness");

            if(meals > 0){

                meals -= 1;
                int random = rand.Next(5,10);
                fullness += random;
                TempData["Message"] = $"You used 1 meal and gain {random} in fullness";
                HttpContext.Session.SetInt32("Meals", (int)meals);
                HttpContext.Session.SetInt32("Fullness", (int)fullness);

                return RedirectToAction("Dojodachi");
            } else {
                TempData["Message"] = "You don't have any meals left";
                return RedirectToAction("Dojodachi");
            }

        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play(){
            Random rand = new Random();
            var energy = HttpContext.Session.GetInt32("Energy");
            var happiness = HttpContext.Session.GetInt32("Happiness");

            if(energy >= 5){
                energy -= 5;
                int random = rand.Next(5,10);
                happiness += random;
                TempData["Message"] = $"You played with dojodachi. Energy -5 and happines +{random}";
                HttpContext.Session.SetInt32("Energy", (int)energy);
                HttpContext.Session.SetInt32("Happiness", (int)happiness);
                return RedirectToAction("Dojodachi");
            } else {

                TempData["Message"] = "You don't have enough energy to play";
                return RedirectToAction("Dojodachi");
            }

        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work(){
            Random rand = new Random();
            var energy = HttpContext.Session.GetInt32("Energy");
            var meals = HttpContext.Session.GetInt32("Meals");

            if(energy >= 5){
                energy -= 5;
                int random = rand.Next(1,3);
                meals += random;
                TempData["Message"] = $"You went to word. Energy -5 and Meals +{random}";
                HttpContext.Session.SetInt32("Energy", (int)energy);
                HttpContext.Session.SetInt32("Meals", (int)meals);
                return RedirectToAction("Dojodachi");

            } else{
                TempData["Message"] = "You don't have enough energy to work";
                return RedirectToAction("Dojodachi");
            }
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep(){
            var energy = HttpContext.Session.GetInt32("Energy");
            var happiness = HttpContext.Session.GetInt32("Happiness");
            var fullness = HttpContext.Session.GetInt32("Fullness");

            energy += 15;
            happiness -= 5;
            fullness -= 5;
            TempData["Message"] = "Went to sleep. Energy +15, Happiness -5, Fullness -5";
            HttpContext.Session.SetInt32("Energy", (int)energy);
            HttpContext.Session.SetInt32("Happiness", (int)happiness);
            HttpContext.Session.SetInt32("Fullness", (int)fullness);

            return RedirectToAction("Dojodachi");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Restart(){
            HttpContext.Session.Clear();
            TempData["Message"] = null;
            return RedirectToAction("Index");
        }

    }
}