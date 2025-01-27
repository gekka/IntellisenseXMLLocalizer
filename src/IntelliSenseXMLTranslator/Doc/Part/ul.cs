using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class ul : GridBase
    {
        public ul(XElement ul) : base(ul, "li", ".")
        {
        }
    }
}
