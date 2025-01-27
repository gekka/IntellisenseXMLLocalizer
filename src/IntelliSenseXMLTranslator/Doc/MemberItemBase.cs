using System.Xml.Linq;
using System.Xml.XPath;
namespace IntellisenseXMLLocalizer.Doc
{
    class MemberItemBase
    {
        public static MemberItemBase? Parse(XElement? element)
        {
            if (element == null)
            {
                return null;
            }
            return new MemberItemBase(element);
        }

        public static MemberItemBase? Parse(XElement? element, string xpath)
        {
            if (element == null)
            {
                return null;
            }
            return Parse(element.XPathSelectElement(xpath));
        }

        public MemberItemBase(XElement element)
        {
            this.element = element;
        }

        internal readonly XElement element;
    }
}
