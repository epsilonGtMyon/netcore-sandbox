using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationSandbox.App
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
