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
using nXPath.Lib.CustomActionFilter;
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

        [NoCache]
        public ActionResult XLoader()
        {
            return View("XLoader");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XLoader(string xmlSourceArea)
        {
            var xml = Server.HtmlEncode(XDocument.Parse(xmlSourceArea).ToString());
            return View("XView", new XmlDocModel() { Xml = xml });
        }

        private string FormatXml(string xml)
        {
            return XDocument.Parse(xml).ToString();
        }

        private string StringifyNode(XmlNode node) {
            string result = String.Empty;
            if (node is XmlElement)
            {
                result = this.FormatXml((node as XmlElement).OuterXml);
            }
            else if (node is XmlText)
            {
                result = (node as XmlText).Value;
            }
            else {
                result = node.ToString();
            }
            return result;
        }

        private XPathMatch BuildXPathMatch(int nodeNum, XmlNode node)
        {
            return new XPathMatch() { 
                Id = nodeNum, 
                Type = node.GetType().ToString(), 
                Content = this.StringifyNode(node)
            };
        }

        [HttpPost]
        public ActionResult XPathProcessing(string xmlDoc, string xPath)
        {
            Dictionary<string, string> matchData = new Dictionary<string, string>();

            List<XPathMatch> results = new List<XPathMatch>();
            var xml = Server.HtmlDecode(xmlDoc);

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);

            var nodeList = xdoc.SelectNodes(xPath);

            for (int i = 0; i < nodeList.Count; i++) 
                results.Add(this.BuildXPathMatch(i, nodeList[i]));

            return Json(results);
        }

        public ActionResult XResolve()
        {
            return View();
        }

    }
}
