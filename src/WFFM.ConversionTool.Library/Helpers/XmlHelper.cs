using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace WFFM.ConversionTool.Library.Helpers
{
    public static class XmlHelper
    {
        public static List<string> GetXmlElementNamesold(string fieldValue)
        {
            List<string> elementNames = new List<string>();
            var xmlDocument = new HtmlDocument();
            xmlDocument.OptionAutoCloseOnEnd = true;
            fieldValue = SanitizeFieldValue(fieldValue);
            try
            {
                xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                foreach (var childNode in xmlDocument.DocumentNode.Descendants().FirstOrDefault().Descendants())
                {
                    elementNames.Add(childNode.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("XmlHelper - GetXmlElementNames - Failed to parse Xml value - Value = " + fieldValue);
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine("See logs for more details in the logs folder.");
                Console.WriteLine();
                throw;
            }

            return elementNames;
        }

        public static List<string> GetXmlElementNames(string fieldValue)
        {
            List<string> elementNames = new List<string>();
            var xmlDocument = new HtmlDocument();
            xmlDocument.OptionAutoCloseOnEnd = true;
            fieldValue = SanitizeFieldValue(fieldValue);
            try
            {
                xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                foreach (HtmlNode childNode in xmlDocument.DocumentNode.Descendants().FirstOrDefault().Descendants())
                {
                    elementNames.Add(childNode.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("XmlHelper - GetXmlElementNames - Failed to parse Xml value - Value = " + fieldValue);
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine("See logs for more details in the logs folder.");
                Console.WriteLine();
                throw;
            }

            return elementNames;
        }

        public static string GetXmlElementValueold(string fieldValue, string elementName, bool throwOnError = false)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(elementName))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                    var elementsByTagName = xmlDocument.DocumentNode.Descendants(elementName);

                    if (elementsByTagName.Count() > 0)
                    {
                        var element = elementsByTagName.FirstOrDefault();
                        return element?.InnerHtml;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - GetXmlElementValue - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                    if (throwOnError)
                    {
                        Console.WriteLine("See logs for more details in the logs folder.");
                        Console.WriteLine();
                        throw;
                    }
                }
            }
            return string.Empty;
        }

        public static string GetXmlElementValue(string fieldValue, string elementName, bool throwOnError = false)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(elementName))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                    var elementsByTagName = xmlDocument.DocumentNode.Descendants(elementName);

                    if (elementsByTagName.Count() > 0)
                    {
                        var element = elementsByTagName.FirstOrDefault();
                        return element?.InnerHtml;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - GetXmlElementValue - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                    if (throwOnError)
                    {
                        Console.WriteLine("See logs for more details in the logs folder.");
                        Console.WriteLine();
                        throw;
                    }
                }
            }
            return string.Empty;
        }

        public static HtmlNode GetXmlElementNode(string fieldValue, string elementName, bool throwOnError = false)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(elementName))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                    var elementsByTagName = xmlDocument.DocumentNode.Descendants(elementName);

                    if (elementsByTagName.Count() > 0)
                    {
                        return elementsByTagName.FirstOrDefault();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - GetXmlElementNode - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                    if (throwOnError)
                    {
                        Console.WriteLine("See logs for more details in the logs folder.");
                        Console.WriteLine();
                        throw;
                    }
                }
            }
            return null;
        }

        public static List<HtmlNode> GetXmlElementNodeListold(string fieldValue, string elementName, bool throwOnError = false)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(elementName))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));

                    var elementsByTagName = xmlDocument.DocumentNode.Descendants(elementName);

                    if (elementsByTagName.Count() > 0)
                    {
                        return elementsByTagName.ToList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - GetXmlElementNodeList - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                    if (throwOnError)
                    {
                        Console.WriteLine("See logs for more details in the logs folder.");
                        Console.WriteLine();
                        throw;
                    }
                }
            }
            return null;
        }

        public static List<HtmlNode> GetXmlElementNodeList(string fieldValue, string elementName, bool throwOnError = false)
        {
            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(elementName))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));
                    var elementsByTagName1 = xmlDocument.DocumentNode.Descendants(elementName);

                    if (elementsByTagName1?.Count() > 0)
                    {
                        return elementsByTagName1.ToList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - GetXmlElementNodeList - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                    if (throwOnError)
                    {
                        Console.WriteLine("See logs for more details in the logs folder.");
                        Console.WriteLine();
                        throw;
                    }
                }
            }
            return null;
        }

        public static string StripHtml(string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
            {
                var xmlDocument = new HtmlDocument();
                xmlDocument.OptionAutoCloseOnEnd = true;
                fieldValue = SanitizeFieldValue(fieldValue);
                try
                {
                    xmlDocument.LoadHtml(AddParentNodeAndEncodeElementValue(fieldValue));
                    return xmlDocument.Text;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("XmlHelper - StripHtml - Failed to parse Xml value - Value = " + fieldValue);
                    Console.WriteLine(e);
                    Console.WriteLine();
                }

            }
            return fieldValue;
        }


        private static string AddParentNodeAndEncodeElementValue(string fieldValue)
        {
            if (!fieldValue.StartsWith("<?xml", StringComparison.InvariantCultureIgnoreCase))
            {
                // Add parent xml element to value
                fieldValue = string.Format("<ParentNode>{0}</ParentNode>", fieldValue);
                // Escape special chars in text value
                fieldValue = fieldValue.Replace(" & ", " &amp; ").Replace(" &<", " &amp;<");
            }

            return fieldValue;
        }

        private static string SanitizeFieldValue(string fieldValue)
        {
            return fieldValue.Replace("<br>", "<br/>")
                .Replace("</em<", "</em><")
                .Replace("</b<", "</b><")
                .Replace("</a<", "</a><")
                .Replace("</strong<", "</strong><")
                .Replace("</i<", "</i><")
                .Replace("&rsquo;", "'")
                .Replace("&lsquo;", "'")
                .Replace("&nbsp;", " ");
        }
    }
}