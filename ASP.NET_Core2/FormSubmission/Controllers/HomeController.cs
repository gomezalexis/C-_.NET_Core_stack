using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FormSubmission.Models;

namespace FormSubmission.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.errors = TempData["Error"];
            ViewBag.success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [Route("adduser")]
        public IActionResult AddUser(string FirstName, string LastName, int Age, string Email, string Password){
            TempData["Error"] = null;
            TempData["Success"] = null;
            
            User newUser = new User{
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Email = Email,
                Password = Password
            };

            if(TryValidateModel(newUser)){
                List<string> success = new List<string>(new string[]{"Success","Welcome User"});

                TempData["Success"] = success;
                return RedirectToAction("Index");
            } else{
                List<string> theErrors = new List<string>();
                foreach(var error in ModelState.Values){
                    if(error.Errors.Count > 0){

                        theErrors.Add(error.Errors[0].ErrorMessage.ToString());
                    }
                }
                TempData["Error"] = theErrors;

                return RedirectToAction("Index");
            }
            
        }
    }
}
