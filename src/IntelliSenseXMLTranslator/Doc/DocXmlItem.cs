namespace Gekka.Language.IntelliSenseXMLTranslator.Doc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml;

    class DocXmlItem
    {
        public DocXmlItem(XmlNode node)
        {
            Node = node;
            IsCodeNode = codetags.Contains(node.Name);

            List<XmlNode> list = new List<XmlNode>();

            foreach (var n in node.XmlNodes())
            {
                if (blocktags.Contains(n.Name))
                {
                    if (list.Count() > 0)
                    {
                        Texts.Add(new TextRange(list.AsReadOnly()));

                    }
                    list = new List<XmlNode>();
                }
                else
                {
                    list.Add(n);
                }
            }

            if (list.Count() > 0)
            {
                Texts.Add(new TextRange(list.AsReadOnly()));
            }


            if (IsCodeNode && list.Count>0)
            { 
            }
        }

        /// <summary>置換対象となるノード(このノードの中身が置換対象)</summary>
        public XmlNode Node { get; }

        public string Tag => Node.Name;

        /// <summary>Nodeに値か子ノードがあるか</summary>
        public bool HasInner { get; set; }

  
        public bool IsCodeNode { get; }

        private static readonly string[] blocktags = { "list", "table", "ul", "br" };

        private static readonly string[] codetags = { "c" , "code" };

        /// <summary>Node内の置換対象範囲一覧</summary>
        public List<TextRange> Texts { get; } = new List<TextRange>();
    }
}
