using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Blog.Modules
{
    [Route("/api")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var resource = new Resource
            {
                Title = "Home",
                HRef = "/api",
                Links = new Link[]
                {
                    new Link {Rel = "articles", HRef = "/api/articles", Text = "Articles"},
                    new Link {Rel = "categories", HRef = "/api/categories", Text = "Categories"}
                }
            };

            return Ok(resource);
        }
    }
}
