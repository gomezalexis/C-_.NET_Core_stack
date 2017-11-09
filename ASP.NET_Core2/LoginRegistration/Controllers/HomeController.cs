using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {   
            ViewBag.errors = TempData["Error"];
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password){
            TempData["Error"] = null;

            Loger theLoger = new Loger{
                Email = Email,
                Password = Password
            };

            if(TryValidateModel(theLoger))
            {
                string query = $"SELECT email, password FROM users WHERE email = '{theLoger.Email}'";
                var theUser = _dbConnector.Query(query);
                if(theUser.Count == 0){
                    string[] message = {"Email not in database"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                } else if(Password != (string)theUser[0]["password"]){
                    string[] message = {"Password didn't match"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                } else{
                    return RedirectToAction("Welcome");
                }
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

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user){
            if(ModelState.IsValid)
            {
                string query = $"INSERT INTO users (firstname, lastname, email, password, created_at) " 
                + $"VALUES ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Password}', NOW())";
                _dbConnector.Execute(query);
                return RedirectToAction("Welcome");
            }
            return View("Index",user);
        }

        [HttpGet]
        [Route("welcome")]
        public IActionResult Welcome(){
            string query = "SELECT * FROM users ORDER BY created_at DESC";
            var AllUsers = _dbConnector.Query(query);
            ViewBag.allUsers = AllUsers;
            return View();
        }

        [HttpGet]
        [Route("delete/{Email}")]
        public IActionResult Delete(string Email){
            Console.WriteLine("Deleting user.");
            string query = $"DELETE FROM users WHERE email = '{Email}'";
            _dbConnector.Query(query);
            return RedirectToAction("Welcome");
        }

    }
}
