using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;

namespace AnonymousId.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IAnonymousIdFeature feature = HttpContext.Features.Get<IAnonymousIdFeature>();
            if (feature != null)
            {
                string anonymousId = feature.AnonymousId;
            }

            return View();
        }
    }
}