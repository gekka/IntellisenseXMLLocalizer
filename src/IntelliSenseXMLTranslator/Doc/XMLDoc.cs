
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.XPath;
namespace IntellisenseXMLLocalizer.Doc
{

    class XMLDoc
    {
        public static Task<XMLDoc> LoadAsync(System.IO.FileInfo file, System.Threading.CancellationToken token)
        {
            return LoadAsync(file.OpenRead(), token);
        }
        public static async Task<XMLDoc> LoadAsync(System.IO.Stream stream, System.Threading.CancellationToken token)
        {

            var xdoc = await System.Xml.Linq.XDocument.LoadAsync(stream, System.Xml.Linq.LoadOptions.PreserveWhitespace, token);

            var ret = new XMLDoc(xdoc);

            return ret;

        }

        private XMLDoc(System.Xml.Linq.XDocument xdoc)
        {
            this.xdoc = xdoc;

            var mems = xdoc.XPathSelectElements("/doc/members/member");
            if (!mems.Any(_ => true))
            {
                throw new ApplicationException("XMLDocではありません");
            }
        }

        private System.Xml.Linq.XDocument xdoc;

        //public IReadOnlyList<Member> members => _members ?? (_members = xdoc.XPathSelectElements("/doc/members/member").Select(_ => new Member(_)).ToList().AsReadOnly());
        //private IReadOnlyList<Member>? _members;
    }

    static class Tool
    {
        public static XAttribute? GetAttribute(this XElement element, string name)
        {
            return (element.XPathEvaluate("@" + name) as IEnumerable)?.OfType<XAttribute>().FirstOrDefault();
        }
    }
}
