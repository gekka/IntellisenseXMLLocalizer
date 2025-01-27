using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class @param : MemberItemBase
    {
        public param(XElement element) : base(element)
        {
            name = element.GetAttribute("name");
        }

        public XAttribute? name { get; }
    }
}
