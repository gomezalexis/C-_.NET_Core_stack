using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using BankAccounts.Models;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private BankContext _context;
        public HomeController(BankContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.userError = TempData["UserError"];
            ViewBag.loginError = TempData["LoginError"];
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserWrapper theUser)
        {
            TempData["UserError"] = null;
            if(ModelState.IsValid)
            {
                List<User> returnedValues = _context.Users.Where(user => user.email == theUser.registerVM.email).ToList();
                System.Console.WriteLine($"The return value count is {returnedValues.Count}");
                if(returnedValues.Count > 0)
                {
                    TempData["UserError"] = "Email already exists in database";
                    return RedirectToAction("Index");
                } else{
                    // starting to hashpassword
                    PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                    theUser.registerVM.password = Hasher.HashPassword(theUser.registerVM, theUser.registerVM.password);
                    // Now assign all values to user object
                    User newUser = new User
                    {
                        firstName = theUser.registerVM.firstName,
                        lastName = theUser.registerVM.lastName,
                        email = theUser.registerVM.email,
                        password = theUser.registerVM.password,
                        balance = 1000.00,
                    };
                    _context.Add(newUser);
                    _context.SaveChanges();
                    //to set session
                    User helloUser = _context.Users.SingleOrDefault(user => user.email == newUser.email);
                    SessionExtensions.SetObjectAsJson(HttpContext.Session, "UserSession", helloUser);
                    return Redirect("/bankaccount");
                }
            }
            return View("Index", theUser);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserWrapper theUser)
        {
            TempData["UserError"] = null;
            if(ModelState.IsValid)
            {
                User comebackUser = _context.Users.SingleOrDefault(user => user.email == theUser.loginVM.loginEmail);
                if(comebackUser != null){
                    System.Console.WriteLine("We are here logged");
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(comebackUser, comebackUser.password, theUser.loginVM.loginPassword))
                    {
                        SessionExtensions.SetObjectAsJson(HttpContext.Session, "UserSession", comebackUser);
                        return Redirect("/bankaccount");
                    } else{
                        TempData["LoginError"] = "Password didn't match";
                        return RedirectToAction("Index");
                    }
                } else
                    TempData["LoginError"] = "Email not in Database";
                    return RedirectToAction("Index");
                

            }
            return View("Index", theUser);
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
