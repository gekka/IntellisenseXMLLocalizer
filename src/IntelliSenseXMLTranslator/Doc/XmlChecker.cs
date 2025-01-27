namespace Gekka.Language.IntelliSenseXMLTranslator.Doc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// XMLファイルの正常性確認
    /// </summary>
    class XmlChecker
    {
        public enum RepairResult
        {
            NoBroken = 0,
            Repaired,

            Broken,

        }

        /// <summary>XMLが不正になっているElementを含んでいる場合に、可能なら修復する</summary>
        /// <param name="originalFile">元のXMLのパス</param>
        /// <param name="repairedXMLFile">修復したXMLのパス</param>
        /// <returns>不正がないか修復出来たらtrue,修復してみても不正のままならfalse</returns>
        public static RepairResult RepairXML(string originalFile, string repairedXMLFile)
        {
            bool hasBroken = true;
            string xml = ReadOriginalXMLString(originalFile);
            for (int i = 0; i < 1 && hasBroken; i++)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                hasBroken = RepaireNode(xml, tw);
                xml = sb.ToString();
            }
            //if (hasBroken)
            //{
            //    throw new ApplicationException("XMLファイルに異常がありますが修復できませんでした");
            //}
            System.IO.File.Delete(repairedXMLFile);

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(repairedXMLFile, false, new System.Text.UTF8Encoding(true)))
            {
                sw.Write(xml);
            }

            return RepaireRoot(repairedXMLFile, hasBroken);
        }

        private static string ReadOriginalXMLString(string originalFile)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(originalFile, true))
            {
                return sr.ReadToEnd();
            }
        }

        private static bool RepaireNode(string xml, System.IO.TextWriter tw)
        {
            bool hasBroken = false;
            //string xml;
            //using (System.IO.StreamReader sr = new System.IO.StreamReader(originalFile, true))
            //{
            //    xml = sr.ReadToEnd();
            //}

            string[] tags = new string[] { "summary", "remarks", "returns", "param", "paramref", "typeparam", "typeparamref", "value", "exception" };
            var pattern = string.Join("|", tags.Select(tagName => $"(<{tagName}.+?</{tagName}>)"));
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.Singleline);

            int lastIndex = 0;



            Dictionary<string, int> dicBrokenTagCount = new Dictionary<string, int>();

            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(repairedXMLFile, false, new System.Text.UTF8Encoding(true)))
            {
                foreach (var match in reg.EnumerateMatches(xml))
                {
                    var text = xml.Substring(match.Index, match.Length);
                    //bool issuccess;
                    try
                    {
                        using (var sr = new System.IO.StringReader(text))
                        {
                            System.Xml.XmlReader xr = System.Xml.XmlReader.Create(sr);
                            while (xr.Read()) { }
                        }

                        continue;
                    }
                    catch (System.Xml.XmlException)
                    {
                    }

                    hasBroken = true;

                    var index = text.IndexOfAny(new char[] { ' ', '>', '/' });
                    var tagName = text.Substring(1, index - 1).Trim();
                    if (tagName != null)
                    {
                        if (dicBrokenTagCount.TryGetValue(tagName, out var count))
                        {
                            dicBrokenTagCount[tagName] = count + 1;
                        }
                        else
                        {
                            dicBrokenTagCount[tagName] = 1;
                        }
                    }
                    tw.Write(xml.AsSpan(lastIndex, match.Index - lastIndex));
                    //sw.Write("<returns>!!! BROKEN !!!</returns>");
                    tw.Write($"<?broken {text} ?>");
                    lastIndex = match.Index + match.Length;

                }

                tw.Write(xml.AsSpan(lastIndex, xml.Length - lastIndex));
                tw.Flush();
            }

            return hasBroken;
        }



        /// <summary>XMLのルートがおかしい状態を修正する</summary>
        /// <param name="repairedXMLFile"></param>
        /// <param name="hasBroken"></param>
        /// <returns></returns>
        private static RepairResult RepaireRoot(string repairedXMLFile, bool hasBroken)
        {
            System.Xml.XmlDocument xdoc;
            try
            {
                xdoc = new System.Xml.XmlDocument();
                xdoc.Load(repairedXMLFile);

                if (DocXml.GetDocMembersNode(xdoc, out var members))
                {
                    return hasBroken ? RepairResult.Repaired : RepairResult.NoBroken;
                }


                // なぜか<?xml><span><doc></doc><span> になっているXMLがある(netstandard.xml ～2.0だよ)
                // <?xml><doc></doc>に修正
                foreach (System.Xml.XmlNode cn0 in xdoc.ChildNodes.XmlNodes())
                {
                    if (cn0 is System.Xml.XmlDeclaration)
                    {
                        continue;
                    }

                    if (DocXml.GetDocMembersNode(cn0, out members))
                    {
                        if (members.Count == 1)
                        {
                            if (members[0].ChildNodes.XmlNodes().Any(_ => _.Name == "member"))
                            {
                                var doc = cn0.SelectSingleNode("doc")!;
                                cn0.RemoveChild(doc);
                                xdoc.RemoveChild(cn0);
                                xdoc.AppendChild(doc);

                                xdoc.Save(repairedXMLFile);

                                return RepairResult.Repaired;
                            }
                        }
                    }
                }

                return RepairResult.Broken;
            }
            catch
            {
                return RepairResult.Broken;
            }
        }
    }


}
