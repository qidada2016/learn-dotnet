using ControllerExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            //return new ContentResult()
            //{
            //    Content = "from index",
            //    ContentType = "text/plain"
            //};

            //return Content("hello world", "text/plain");

            return Content("<h1>hello world</h1>", "text/html");
        }

        [Route("person")]
        public JsonResult Person()
        {
            //return "{\"key\":\"value\"}";

            var person = new Person() { Id = Guid.NewGuid(), Age = 18, FirstName = "Tom", LastName = "Cat" };
            //return new JsonResult(person);

            return Json(person);
        }

        [Route("file")]
        public VirtualFileResult FileDownload()
        {

            //return new VirtualFileResult("/sample.pdf", "application/pdf");

            return File("/sample.pdf", "application/pdf");
        }

        [Route("phone/{phone:regex(^\\d{{10}}$)}")]
        public string Phone()
        {
            return "hello from method1";
        }
    }
}
