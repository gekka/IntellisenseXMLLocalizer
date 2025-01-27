using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using System.Xml.XPath;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class GridBase : ValuePartBase
    {
        public GridBase(XElement list, string rowXPath, string cellXPath) : base(list, NodeMapType.Group)
        {
            Rows = list.XPathSelectElements(rowXPath)
                .Select(_ => new GridRow(_, cellXPath))
                .ToList().AsReadOnly();

        }

        public IReadOnlyList<GridRow> Rows { get; }
    }
}
