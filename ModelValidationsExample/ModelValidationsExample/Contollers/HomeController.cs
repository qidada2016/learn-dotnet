using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Contollers
{
    public class HomeController : Controller
    {
        [Route("register")]
        public IActionResult Index(Person person)
        {

            if (!ModelState.IsValid)
            {
                //var errorList = new List<string>();
                //foreach (var value in ModelState.Values)
                //{
                //    foreach (var error in value.Errors)
                //    {
                //        errorList.Add(error.ErrorMessage);
                //    }
                //}

                var errorList = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();

                var errors = string.Join("\n", errorList);
                return BadRequest(errors);
            }

            return Content($"{person}");
        }
    }
}
