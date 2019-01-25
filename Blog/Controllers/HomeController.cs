using System.Linq;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private DB db = new DB();

        public ActionResult Index()
        {
            var recipes = db.Articles.ToList();
            return View(recipes);
        }
    }
}