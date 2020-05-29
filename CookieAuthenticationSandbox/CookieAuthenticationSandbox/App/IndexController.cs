using Microsoft.AspNetCore.Mvc;

namespace CookieAuthenticationSandbox.App
{
    [Route("/")]
    public class IndexController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
}
