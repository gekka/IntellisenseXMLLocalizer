using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class table : GridBase
    {
        public table(XElement list) : base(list, "thread/tr | tbody/tr", "th | td")
        {
        }
    }
}
