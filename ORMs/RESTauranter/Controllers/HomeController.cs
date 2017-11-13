using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RESTauranter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RESTauranter.Controllers
{
    public class HomeController : Controller
    {
        private FirstContext _context;
        public HomeController(FirstContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.error = TempData["DateError"];
            return View();
        }

        [HttpGet]
        [Route("/showallreviews")]
        public IActionResult AllReviews(){
            
            List<Review> allReviews = _context.Reviews.ToList();
            // List<Review> allReviews = _context.Reviews.Where(reviews => reviews.dateOfVisit)
            ViewBag.allReviews = allReviews;
            return View();
        }

        [HttpPost]
        [Route("addreview")]
        public IActionResult AddReview(Review review)
        {
            TempData["DateError"] = null;

            if(ModelState.IsValid){
                if(review.dateOfVisit > DateTime.Now)
                {
                    TempData["DateError"] = "Date can't be greater than today!";
                    return RedirectToAction("Index");
                }
                
                // review.dateOfVisit = review.dateOfVisit.Date; 
                _context.Add(review);
                _context.SaveChanges();
                return Redirect("/showallreviews");
            }

            return View("Index", review);

        }
    }
}
