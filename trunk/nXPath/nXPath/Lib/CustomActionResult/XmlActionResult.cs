using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace nXPath.Lib.CustomActionResult
{
    // from: http://stackoverflow.com/questions/134905/what-is-the-best-way-to-return-xml-from-a-controllers-action-in-asp-net-mvc/12718046#12718046
    public sealed class XmlActionResult : ActionResult
    {
        private readonly XDocument _document;

        public System.Xml.Formatting Formatting { get; set; }
        public string MimeType { get; set; }
        public Encoding Encoding { get; set; }

        public XmlActionResult(XDocument document, Encoding encoding = null)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;

            // Default values
            MimeType = "text/xml";
            Formatting = System.Xml.Formatting.None;
            Encoding = encoding ?? Encoding.UTF8;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = MimeType;

            using (var writer = new XmlTextWriter(context.HttpContext.Response.OutputStream, this.Encoding) { Formatting = Formatting })
                _document.WriteTo(writer);
        }
    }

}