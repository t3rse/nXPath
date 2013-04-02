using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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
           
            List<string> results = new List<string>();
            var xml = Server.HtmlDecode(xmlDoc);

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);

            var nodeList = xdoc.SelectNodes(xPath);

            foreach (var node in nodeList) {
                if (node is XmlElement)
                {
                    results.Add((node as XmlElement).OuterXml);
                }
                else if (node is XmlText)
                {
                    results.Add((node as XmlText).Value);
                }
                else {
                    results.Add(String.Format("{0}::{1}", node.GetType().ToString(), node.ToString()));
                }
            }

            return Content(String.Join("<br/>", results.Select(r => r.ToString()).ToArray()));
        }

        public ActionResult XResolve() {
            return View();
        }

    }
}
