using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class @exception : MemberItemBase
    {
        public exception(XElement element) : base(element)
        {
            cref = element.GetAttribute("cref");
        }

        public XAttribute? cref { get; }
    }
}
