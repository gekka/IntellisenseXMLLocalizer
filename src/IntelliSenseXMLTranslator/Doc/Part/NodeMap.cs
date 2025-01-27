using System.Xml.Linq;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class NodeMap : ValuePartBase
    {
        public NodeMap(XNode node, NodeMapType mapType) : base(node, mapType)
        {
        }
        //public NodeMap(XNode node, string? mapkey) : base(node)
        //{
        //    MapKey = mapkey;
        //}


    }

    class NodeMapGroup : ValuePartBase
    {
        public NodeMapGroup(XNode node) : base(node, NodeMapType.Group)
        {
        }
    }

    enum NodeMapType
    {

        Skip,

        Text,

        ChildElements,

        Replace,
        Inner,
        NewLine,

        Group,
    }


}
