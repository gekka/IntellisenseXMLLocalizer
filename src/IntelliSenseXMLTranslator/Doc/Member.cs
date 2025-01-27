
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using System.Xml.XPath;
using IntellisenseXMLLocalizer.Doc.Part;
namespace IntellisenseXMLLocalizer.Doc
{
    class Member : IChildElements
    {
        public Member(XElement element)
        {
            this.element = element;

            this.name = (element.XPathEvaluate("@" + nameof(name)) as IEnumerable)?.OfType<XAttribute>().FirstOrDefault();
            this.summary = MemberItemBase.Parse(element, nameof(summary));
            this.returns = MemberItemBase.Parse(element, nameof(returns));
            this.remarks = MemberItemBase.Parse(element, nameof(remarks));

            this.@params = element.XPathSelectElements("param").Select(_ => new param(_)).ToList().AsReadOnly();
            this.typeparams = element.XPathSelectElements("typeparam").Select(_ => new param(_)).ToList().AsReadOnly();
            this.exceptions = element.XPathSelectElements("exception").Select(_ => new exception(_)).ToList().AsReadOnly();

            this.examples = element.XPathSelectElements("example").Select(_ => new example(_)).ToList().AsReadOnly();

            this.ChildElements = new MemberItemBase?[] { summary, returns, remarks }
                .Union(this.@params)
                .Union(this.typeparams)
                .Union(this.exceptions)
                .Union(this.examples)
                .OfType<MemberItemBase>().Select(_ => _.element)
                .ToArray().AsReadOnly();

#if DEBUG
            var y = element.Elements().Except(ChildElements).ToArray();
            var z = ChildElements.Except(element.Elements()).ToArray();
            if (y.Length > 0 || z.Length > 0)
            {
                throw new ApplicationException("<member>タグの子に想定外のタグがあります");
            }
#endif
        }

        private XElement element;

        public XAttribute? name { get; }

        public MemberItemBase? summary { get; }
        public MemberItemBase? returns { get; }
        public MemberItemBase? remarks { get; }

        public IReadOnlyList<param> @params { get; }
        public IReadOnlyList<param> typeparams { get; }
        public IReadOnlyList<exception> @exceptions { get; }

        public IReadOnlyList<example> @examples { get; }

        public IReadOnlyList<XElement> ChildElements { get; }

        IEnumerable<XElement> IChildElements.ChildElements => ChildElements;
    }


    interface IChildElements
    {
        IEnumerable<XElement> ChildElements { get; }
    }

    static class ChildElementsTool
    {
        public static IEnumerable<IList<ValuePartBlock>> Split(this IChildElements source, bool addInnerCell = true)
        {
            return Split(source.ChildElements, addInnerCell);
        }
        public static IEnumerable<IList<ValuePartBlock>> Split(this IEnumerable<XElement> source, bool addInnerCell = true)
        {
            foreach (var e in source)
            {
                var ev = new Doc.ElementValue(e);

                var blocks = Split(ev.Parts, addInnerCell);
                yield return blocks;
            }
        }

        public static IList<ValuePartBlock> Split(IEnumerable<ValuePartBase> source, bool addInnerCell = true)
        {
            List<ValuePartBlock> ret = new List<ValuePartBlock>();

            ValuePartBlock? block = null;

            foreach (var vp in source)
            {
                var elem = vp.node as XElement;

                switch (vp.MapType)
                {
                case NodeMapType.Skip:
                    break;
                case NodeMapType.Text:
                    break;
                case NodeMapType.ChildElements:

                    if (vp is IChildElements ce)
                    {
                        foreach (var innerBlocks in Split(ce))
                        {
                            ret.AddRange(innerBlocks);
                        }
                        continue;
                    }


                    break;
                case NodeMapType.Replace:
                    ValuePartBlock temp = new ValuePartBlock();
                    temp.list.Add(vp);

                    ret.Add(temp);
                    break;
                case NodeMapType.Inner:
                    break;
                case NodeMapType.NewLine:
                    block = null;
                    continue;

                case NodeMapType.Group:
                    if (vp is GridBase grid)
                    {
                        foreach (var innerBlocks in Split(grid.Rows.SelectMany(_ => _.Cells)))
                        {
                            ret.AddRange(innerBlocks);
                        }
                    }
                    else
                    {
                        if (elem != null)
                        {
                            foreach (var innerBlocks in Split(elem.Elements()))
                            {
                                ret.AddRange(innerBlocks);
                            }

                        }
                        else
                        {
                        }
                    }
                    block = null;
                    continue;
                default:
                    break;
                }
                //if (vp is GridBase grid)
                //{
                //    if (addInnerCell)
                //    {
                //        //if (true)
                //        //{
                //        //    block = new ValuePartBlock();
                //        //    ret.Add(block);
                //        //}

                //        foreach (var innerBlocks in Split(grid.Rows.SelectMany(_ => _.Cells)))
                //        {
                //            ret.AddRange(innerBlocks);
                //        }
                //    }
                //    block = null;
                //}
                //else
                {
                    if (block == null)
                    {
                        block = new ValuePartBlock();
                        ret.Add(block);
                    }

                    block.list.Add(vp);
                }
            }


            return ret;

        }
    }
}
