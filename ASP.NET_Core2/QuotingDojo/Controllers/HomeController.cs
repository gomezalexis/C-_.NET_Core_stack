using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuotingDojo.Connectors;


namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        private MySqlConnector cnx;
        public HomeController(){
            cnx = new MySqlConnector();
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("addquote")]
        public IActionResult AddQuote(string Quote, string Author){
            System.Console.WriteLine("Quote - " + Quote);
            System.Console.WriteLine("Author - " + Author);
            string query = $"INSERT INTO quotes (quote, author, created_at) VALUES ('{Quote}', '{Author}', NOW())";
            cnx.Execute(query);
            return RedirectToAction("Quotes");
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult Quotes(){
            string query = "SELECT * FROM quotes ORDER BY created_at DESC";
            var AllQuotes = cnx.Query(query);
            ViewBag.allQuotes = AllQuotes;
            // List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes");
            return View();
        }
    }
}
