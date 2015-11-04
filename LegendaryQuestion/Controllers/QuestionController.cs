using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult Index()
        {
            return View(); 
        }
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}