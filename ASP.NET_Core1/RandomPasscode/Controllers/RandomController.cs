using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
 
namespace RandomPasscode.Controllers{
    public class RandomController : Controller{

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            if (HttpContext.Session.GetInt32("Times") == null){
                HttpContext.Session.SetInt32("Times",0);
            }

            int? IntVariable = HttpContext.Session.GetInt32("Times") + 1;
            HttpContext.Session.SetInt32("Times", (int)IntVariable);

            string selector = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            string passcode = "";

            Random rand = new Random();

            for(int i = 0; i < 14; i++){
                int index = rand.Next(0,selector.Length - 1);
                passcode += selector[index];
            }

            ViewBag.thePasscode = passcode;
            ViewBag.theNumber = IntVariable;
            return View();
        }

        [HttpPost]
        [Route("increase")]
        public IActionResult Increase(){
            
            
            return RedirectToAction("Index");
        }
    }
}