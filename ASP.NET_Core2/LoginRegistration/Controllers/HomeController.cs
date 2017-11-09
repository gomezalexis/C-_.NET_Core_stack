using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using Newtonsoft.Json;


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
                string query = $"SELECT email, password, firstname FROM users WHERE email = '{theLoger.Email}'";
                var theUser = _dbConnector.Query(query);
                if(theUser.Count == 0){
                    //veryfing if email is in database
                    string[] message = {"Email not in database"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                    //if email exists in database veryfing if password is correct
                } else if(Password != (string)theUser[0]["password"]){
                    string[] message = {"Password didn't match"};
                    TempData["Error"] = message;
                    return RedirectToAction("Index");
                } else{
                    // Setting the session as an Json String to desirialize it later
                    SessionExtensions.SetObjectAsJson(HttpContext.Session,"UserSession", theUser[0]);
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
                //Bring the user back from database to use in session
                string query2 = $"SELECT email, password, firstname FROM users WHERE email = '{user.Email}'";
                var theUser = _dbConnector.Query(query2);
                SessionExtensions.SetObjectAsJson(HttpContext.Session,"UserSession", theUser[0]);
                return RedirectToAction("Welcome");
            }
            return View("Index",user);
        }

        [HttpGet]
        [Route("welcome")]
        public IActionResult Welcome(){
            // Attaching all the properties of the query to an Object of User
            User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            // Querying for all the users except the on in session
            string query = $"SELECT * FROM users WHERE NOT email = '{theUser.Email}' ORDER BY created_at DESC";
            var AllUsers = _dbConnector.Query(query);
            // string theUser = HttpContext.Session.GetString("UserSession");
            
            ViewBag.theUser = theUser;
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

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
