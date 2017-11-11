using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using Newtonsoft.Json;

namespace TheWall.Controllers
{
    public class UserController : Controller
    {
        private readonly DbConnector _dbConnector;
        public UserController(DbConnector connect)
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
                string query = $"SELECT email, password, firstname, id FROM users WHERE email = '{theLoger.Email}'";
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
                string query2 = $"SELECT email, password, firstname, id FROM users WHERE email = '{user.Email}'";
                var theUser = _dbConnector.Query(query2);
                SessionExtensions.SetObjectAsJson(HttpContext.Session,"UserSession", theUser[0]);
                return RedirectToAction("Welcome");
            }
            return View("Index",user);
        }

        [HttpGet]
        [Route("welcome")]
        public IActionResult Welcome(){
            User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            List<Message> allStuff = new List<Message>();
            string query = $"SELECT messages.id, CONCAT(users.firstname, ' ', users.lastname) AS fullname, messages.created_at, messages.message " 
                + "FROM messages JOIN users ON users.id = messages.user_id ORDER BY created_at DESC";
            var AllMessages = _dbConnector.Query(query);
            foreach(var message in AllMessages){
                Console.WriteLine("The id is " + message["id"]);
                string query2 = "SELECT comments.comment, concat(users.firstname, ' ', users.lastname) AS fullname, comments.created_at " +
                $"FROM comments JOIN users ON comments.user_id = users.id WHERE comments.message_id = {message["id"]}";
                var allComments = _dbConnector.Query(query2);
                    // if(allComments.Count > 0){
                        message.Add("comments", allComments);
                    // }
            }
            ViewBag.theUser = theUser;
            ViewBag.allMessages = AllMessages;
            ViewBag.messageError = TempData["Error"];
            ViewBag.commentError = TempData["CommentError"];
            return View();
        }

        [HttpPost]
        [Route("addmessage")]
        public IActionResult AddMessage(string Message){
            // User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            TempData["Error"] = null;
            Message theMessage = new Message{
                message = Message
            };

            if(TryValidateModel(theMessage))
            {
                User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
                string query = $"INSERT INTO messages (user_id, message, created_at, updated_at) " +
                $"VALUES ({theUser.id}, '{theMessage.message}', NOW(), NOW())";
                _dbConnector.Execute(query);
                return RedirectToAction("Welcome");
            } else{
                List<string> theErrors = new List<string>();
                foreach(var error in ModelState.Values){
                    if(error.Errors.Count > 0){

                        theErrors.Add(error.Errors[0].ErrorMessage.ToString());
                    }
                }
                TempData["Error"] = theErrors;
                return RedirectToAction("Welcome");
            }
        }

        [HttpPost]
        [Route("addcomment/{Id}")]
        public IActionResult AddComment(string Comment , int Id){
            TempData["CommentError"] = null;
            Comment theComment = new Comment{
                comment = Comment
            };

            if(TryValidateModel(theComment))
            {
                User theUser = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
                string query = $"INSERT INTO comments (user_id, message_id, comment, created_at, updated_at) " +
                $"VALUES ({theUser.id}, {Id}, '{theComment.comment}', NOW(), NOW())";
                _dbConnector.Execute(query);
                return RedirectToAction("Welcome");
            } else{
                List<string> theErrors = new List<string>();
                foreach(var error in ModelState.Values){
                    if(error.Errors.Count > 0){

                        theErrors.Add(error.Errors[0].ErrorMessage.ToString());
                    }
                }
                TempData["CommentError"] = theErrors;
                return RedirectToAction("Welcome");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
