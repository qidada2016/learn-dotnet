using IActionResultExample.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{bookid?}/{isloggedin?}")]
        public IActionResult Index([FromQuery]int? bookid, Book book)
        {

            if (!bookid.HasValue)
            {
                //Response.StatusCode = 400;
                //return Content("bookid缺失");

                return BadRequest("bookid缺失");
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("bookid不能为空");

                return BadRequest("bookid不能为空");
            }

            int bookId = Convert.ToInt16(Request.Query["bookid"]);
            if (bookId <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("bookid不能小于0");

                return BadRequest("bookid不能小于0");
            }
            if (bookId > 1000)
            {
                //Response.StatusCode = 400;
                //return Content("bookid不能大于1000");

                return NotFound("bookid不能大于1000");
            }

            string? isLoggedin = Request.Query["isloggedin"];
            if (string.IsNullOrWhiteSpace(isLoggedin) || Convert.ToBoolean(isLoggedin) == false)
            {
                //Response.StatusCode = 401;
                //return Content("未登录");

                //return Unauthorized("未登录");

                return StatusCode(401);

            }

            //return File("/sample.pdf", "application/pdf");

            // 当前路由发生改变(bookstore -> store/books), 用户已保存为标签, 此时为不影响用户访问失败, 用到重定向功能
            //return new RedirectToActionResult("Books", "Store", new { });

            // 302的简写
            //return RedirectToAction("Books", "Store");

            // 301的简写
            return RedirectToActionPermanent("Books", "Store", new { id = bookId });

            //return new LocalRedirectResult($"store/books/{bookId}");
        }
    }
}
