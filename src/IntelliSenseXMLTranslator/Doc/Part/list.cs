using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class list : GridBase
    {
        public list(XElement list) : base(list, "listheader | item", "term | description")
        {
        }
    }
}
