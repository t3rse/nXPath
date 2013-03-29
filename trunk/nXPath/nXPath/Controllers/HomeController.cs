using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.XPath;
using nXPath.Lib.CustomActionResult;
using nXPath.Models;

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
            var xml = Server.HtmlEncode(XDocument.Parse(xmlSourceArea).ToString());
            return View("XView", new XmlDocModel() { Xml = xml });
        }

        [HttpPost]
        public ActionResult XPathProcessing(string xmlDoc, string xPath) {
            var xml = Server.HtmlDecode(xmlDoc);
            XDocument doc = XDocument.Parse(xml);
            
            return Content("OK");
        }

    }
}
