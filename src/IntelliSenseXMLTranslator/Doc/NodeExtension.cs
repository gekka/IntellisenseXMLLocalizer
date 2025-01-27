namespace Gekka.Language.IntelliSenseXMLTranslator.Doc
{
    using System.Linq;
    using System.Collections.Generic;

    static class NodeExtension
    {
        public static IEnumerable<System.Xml.XmlNode> XmlNodes(this System.Xml.XmlNode node)
        {
            return node.ChildNodes.XmlNodes();
        }
        public static IEnumerable<System.Xml.XmlNode> XmlNodesNotText(this System.Xml.XmlNode node)
        {
            return node.ChildNodes.XmlNodesNotText();
        }

        public static IEnumerable<System.Xml.XmlNode> XmlNodes(this System.Xml.XmlNodeList list)
        {
            return list.OfType<System.Xml.XmlNode>();
        }

        public static IEnumerable<System.Xml.XmlNode> XmlNodesNotText(this System.Xml.XmlNodeList list)
        {
            return list.OfType<System.Xml.XmlNode>().Where(_ => _ is not System.Xml.XmlText);
        }
    }
}
