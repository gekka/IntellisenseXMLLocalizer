using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using System.Xml.XPath;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class GridRow : ValuePartBase,IChildElements
    {
        public GridRow(XElement row, string cellXPath) : base(row,  NodeMapType.ChildElements)
        {
            Cells = row.XPathSelectElements(cellXPath).ToList().AsReadOnly();
        }

        public IReadOnlyList<XElement> Cells { get; }

        IEnumerable<XElement> IChildElements.ChildElements => Cells;
    }
}
