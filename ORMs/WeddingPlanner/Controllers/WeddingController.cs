using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using System.Linq;


namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;
        public WeddingController(WeddingContext context){
            _context = context;
        }

        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            //getting user in session
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            User theUser = _context.Users.SingleOrDefault(user => user.userId == theSession.userId);
            //getting all weddings
            List<Wedding> allWeddings = _context.Weddings
                .Include(w => w.guests)
                    .ThenInclude(g => g.user )
                .ToList();

            ViewBag.theUser = theUser;
            ViewBag.allWeddings = allWeddings;

            
            return View();
        }

        [HttpGet]
        [Route("/newwedding")]
        public IActionResult NewWedding()
        {
            //getting user in session
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            User theUser = _context.Users.SingleOrDefault(user => user.userId == theSession.userId);

            ViewBag.theUser = theUser;
            return View();
        }

        [HttpPost]
        [Route("/addwedding")]
        public IActionResult AddWedding(Wedding wedding){
            //getting user in session
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            ViewBag.theUser = theSession;

            if(ModelState.IsValid)
            {
                Wedding newWedding = new Wedding
                {
                    wedderOne = wedding.wedderOne,
                    wedderTwo = wedding.wedderTwo,
                    weddingDate = wedding.weddingDate,
                    address = wedding.address,
                    userId = theSession.userId
                };
                _context.Add(newWedding);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("NewWedding", wedding);
        }

        [HttpGet]
        [Route("/confirm/{weddId}")]
        public IActionResult ConfirmUser(int weddId)
        {
            //Bringing the session user
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            Guest newGuest = new Guest
            {
                userId = theSession.userId,
                weddingId = weddId
            };
            _context.Add(newGuest);
            _context.SaveChanges();

            return RedirectToAction("dashboard");
        }

        [HttpGet]
        [Route("/unconfirm/{weddId}")]
        public IActionResult UnconfirmUser(int weddId)
        {
            //Bringing the session user
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            Guest removeGuest = _context.Guests
                .SingleOrDefault(guest => guest.userId == theSession.userId && guest.weddingId == weddId );
            
            _context.Guests.Remove(removeGuest);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("/delete/{weddId}")]
        public IActionResult DeleteWedding(int weddId)
        {
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            Wedding deleteWedding = _context.Weddings
                .SingleOrDefault(wedding => wedding.weddingId == weddId);
            
            _context.Weddings.Remove(deleteWedding);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("show/{weddId}")]
        public IActionResult ShowWedding(int weddId)
        {
            //Bringing the session
            User theSession = SessionExtensions.GetObjectFromJson<User>(HttpContext.Session,"UserSession");
            Wedding theWedding = _context.Weddings.Include(g => g.guests)
                .ThenInclude(u => u.user)
                .SingleOrDefault(wedding => wedding.weddingId == weddId);
            
            // List<Guest> theGuests = _context.Users
            //     .Where
            ViewBag.theWedding = theWedding;
            return View();
        }

    }
}