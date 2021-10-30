using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Blog.Modules
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var resource = new Resource
            {
                Title = "Home",
                HRef = "/",
                Links = new Link[]
                {
                    new Link {Rel = "articles", HRef = "/articles", Text = "Articles"},
                    new Link {Rel = "categories", HRef = "/categories", Text = "Categories"}
                }
            };

            return Ok(resource);
        }
    }
}
