using System.Xml.Linq;
using System.Xml.XPath;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class ValuePartBase
    {
        //public static ValuePartBase? Parse(XNode? element)
        //{
        //    if (element == null)
        //    {
        //        return null;
        //    }
        //    return new ValuePartBase(element);
        //}

        //public static ValuePartBase? Parse(XNode? element, string xpath)
        //{
        //    if (element == null)
        //    {
        //        return null;
        //    }
        //    return Parse(element.XPathSelectElement(xpath));
        //}

        public ValuePartBase(XNode node,NodeMapType mapType)
        {
            this.node = node;
            this.MapType = mapType;
        }

        internal readonly XNode node;

        public string? MapKey { get; set; }
        public NodeMapType MapType { get; }

        public ValuePartBase Copy()
        {
            var clone = (ValuePartBase)this.MemberwiseClone();
            clone.MapKey = null;
            return clone;

        }
    }

    class TextPart : ValuePartBase
    {

        public TextPart(XNode element) : base(element, NodeMapType.Text)
        {
        }
    }
}
