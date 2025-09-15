using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books/{id}")]
        public IActionResult Books(int? id)
        {
            //return new VirtualFileResult("/sample.pdf", "application/pdf");

            return Content($"real url {id}");
        }
    }
}
