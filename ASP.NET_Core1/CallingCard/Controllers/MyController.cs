using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallingCard.Controllers{
    public class MyController : Controller{
        
        [HttpGet]
        [Route("")]
        public string Home(){
            return "This is Home page of Calling Card. Enter name/lastName/age/color";
        }

        [HttpGet]
        [Route("{FirstName}/{LastName}/{Age}/{FavoriteColor}")]
        public JsonResult DisplaySomeone(string FirstName, string LastName, int Age, string FavoriteColor){
            var AnonObject = new {
                firstName = FirstName,
                lastName = LastName,
                age = Age,
                favoriteColor = FavoriteColor
            };
            return Json(AnonObject);    
        }
    }
}