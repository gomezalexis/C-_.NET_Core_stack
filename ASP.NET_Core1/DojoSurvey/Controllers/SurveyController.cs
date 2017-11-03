using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DojoSurvey.Controllers{
    public class SurveyController : Controller {

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        [Route("displayanswers")]
        public IActionResult Result(string TheName, string Location, string FavoriteLanguage, string Comment){
            ViewBag.name = TheName;
            ViewBag.location = Location;
            ViewBag.favoriteLanguage = FavoriteLanguage;
            ViewBag.comment = Comment;

            return View();
        }
    }
}