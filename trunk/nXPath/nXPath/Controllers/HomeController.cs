using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nXPath.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XmlLoader() {

            return View("XLoader");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XmlLoader(string xmlSourceArea)
        {
            return Content(xmlSourceArea, "text/xml");
        }
    }
}
