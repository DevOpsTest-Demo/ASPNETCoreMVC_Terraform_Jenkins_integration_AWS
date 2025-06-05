using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("book")]
    public IActionResult Index()
    {
        //bookid should be supplied
        if (!Request.Query.ContainsKey("bookid"))
        {
            return BadRequest("Book-Id is not supplied....");
        }

        //Book Id cant be empty
        if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
        {
            return BadRequest("Book-Id is not null or empty....");
        }

        //Book Id should be between 1 to 1000
        int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookid"]);
        if (bookId <= 0)
        {
            return BadRequest("Book-Id can not be less than or equal to zero....");
        }

        if (bookId > 1000)
        {
            return NotFound("Book-Id can not be greater than or equal 1000....");
        }
        return File("/Sample.png", "application/image");
    }
}