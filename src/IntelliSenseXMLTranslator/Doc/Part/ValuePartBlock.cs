using System;
using System.Linq;
using System.Collections.Generic;
namespace IntellisenseXMLLocalizer.Doc.Part
{
    class ValuePartBlock
    {
        public List<ValuePartBase> list { get; } = new List<ValuePartBase>();

        public void ResetMapKey()
        {
            int num = 0;
            foreach (var item in this.list)
            {
                num++;
                if (item.node.NodeType == System.Xml.XmlNodeType.Text)
                {
                    if (item.node is System.Xml.Linq.XText txt)
                    {
                        item.MapKey = null;
                    }
                    else
                    {
                        item.MapKey = ("Q" + num + "Q");
                    }

                }
                else
                {
                    item.MapKey = ("Q" + num + "Q");
                }
            }
        }

        public string TranslateSource
        {
            get
            {
                ResetMapKey();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int num = 1;
                foreach (var item in list)
                {
                    if (item.node.NodeType == System.Xml.XmlNodeType.Text)
                    {
                        if (item.node is System.Xml.Linq.XText txt)
                        {
                            sb.Append(txt.Value);
                        }
                        else
                        {
                            sb.Append(item.MapKey);
                            num++;
                        }

                    }
                    else
                    {
                        sb.Append(item.MapKey);
                        num++;
                    }
                }

                return sb.ToString();
            }
        }


    }
}
