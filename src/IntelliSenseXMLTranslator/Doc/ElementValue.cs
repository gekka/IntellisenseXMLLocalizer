
using System;
using System.Collections.Generic;

using System.Xml.Linq;
using IntellisenseXMLLocalizer.Doc.Part;
namespace IntellisenseXMLLocalizer.Doc
{
    class ElementValue
    {
        private static List<ValuePartBase> GetValueParts(IEnumerable<XNode> nodes)
        {
            var Parts = new List<ValuePartBase>();

            //int num = 1;
            foreach (var n in nodes)
            {
                if (n is XText)
                {
                    Parts.Add(new TextPart(n));
                }
                else if (n is XElement elem)
                {
                    bool hasValue = !string.IsNullOrWhiteSpace(elem.Value);

                    switch (elem.Name.LocalName)
                    {
                    case "see":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;
                    case "c":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;
                    case "code":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;
                    case "para":

                        Parts.Add(new NodeMapGroup(n));
                        break;
                    case "paramref":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;
                    case "typeparamref":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;
                    case "xref":
                        Parts.Add(new NodeMap(n, NodeMapType.Replace));
                        break;

                    case "br":
                        Parts.Add(new NodeMap(n, NodeMapType.NewLine));
                        break;
                    case "p":
                        Parts.Add(new NodeMapGroup(n));
                        break;
                    case "em":
                        Parts.Add(new NodeMap(n, NodeMapType.Inner));
                        break;

                    case "list":
                        var list = new list(elem);
                        Parts.Add(list);
                        continue;

                    case "ul":
                        var ul = new ul(elem);
                        Parts.Add(ul);
                        continue;

                    case "table":
                        var table = new table(elem);
                        Parts.Add(table);
                        continue;


                    default:
                        throw new ApplicationException();

                    }

                    //NodeMap map = new NodeMap(n);
                    //Parts.Add(map);

                    //num++;
                }
                else
                {
                    throw new ApplicationException();
                }
            }
            return Parts;
        }

        public ElementValue(XElement e)
        {
            this.Element = e;
            Parts = GetValueParts(e.Nodes());
        }

        public XElement Element { get; }

        /// <summary>対象のエレメントの値の要素一覧</summary>
        public List<ValuePartBase> Parts { get; }

        ///// <summary>対象のエレメントの値の要素を翻訳可能な単位に分割</summary>
        ///// <returns></returns>
        //public IEnumerable<ValuePartBlock> GetBlocks()
        //    => ValuePartBlock.Split(this.Parts);
    }
}
