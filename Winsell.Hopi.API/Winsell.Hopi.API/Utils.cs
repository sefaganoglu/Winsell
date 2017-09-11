using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Winsell.Hopi.API
{
    public static class Utils
    {
        public static XDocument RemoveNamespaces(XDocument oldXml)
        {
            try
            {
                var parsed = Regex.Replace(
                    oldXml.ToString().Replace("-", ""),
                    @"(xmlns:?[^=]*=[""][^""]*[""])",
                    "",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);

                parsed = Regex.Replace(parsed, @"\<([A-Za-z])\w+\:((\w+)([\s]+)|(\w+))\>", "<$2>");
                parsed = Regex.Replace(parsed, @"\<\/([A-Za-z])\w+\:((\w+)([\s]+)|(\w+))\>", "</$2>");
                parsed = Regex.Replace(parsed, @"\<\/([A-Za-z])\w+\:((\w+)([\s]+)|(\w+))\>", "<$2 />");

                parsed = parsed.Replace("SOAPENV:", "");
                parsed = parsed.Replace("SOAP-ENV:", "");
                parsed = parsed.Replace("ns2:", "");

                XDocument newXml = XDocument.Parse(parsed);
                return newXml;
            }
            catch (XmlException error)
            {
                throw new XmlException(error.Message + " at Utils.RemoveNamespaces");
            }
        }

        public static XDocument RemoveNamespaces(string oldXml)
        {
            XDocument newXml = XDocument.Parse(oldXml);
            return RemoveNamespaces(newXml);
        }
    }
}