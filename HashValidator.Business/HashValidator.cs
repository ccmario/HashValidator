using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HashValidator.Business
{
    public class HashValidator
    {
        private static readonly Encoding _defaultEnconding = Encoding.GetEncoding("iso-8859-1");
        private static readonly string _urlSchema = "http://www.ans.gov.br/padroes/tiss/schemas";
        private static readonly string _epilogo = "epilogo";
        private static readonly string _hash = "hash";

        private static Stream GenerateHashContent(XDocument xml, XNamespace nameSpace)
        {
            xml.Root.Descendants(nameSpace + _epilogo).Remove();
            xml.Root.Descendants(nameSpace + _hash).Remove();

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream, _defaultEnconding, 1024);

            foreach (var item in xml.Root.Descendants().Where(x => !x.HasElements).Select(element => element.Value))
            {
                streamWriter.Write(item.Replace("&", "&amp;").Replace("<", "&lt;"));
            }

            streamWriter.Flush();
            memoryStream.Flush();

            return memoryStream;
        }

        private static XNamespace GetNamespace(XDocument xml)
        {
            var prefix =
                string.IsNullOrEmpty(xml.Root.GetPrefixOfNamespace(_urlSchema))
                    ? string.Empty
                    : xml.Root.GetPrefixOfNamespace(_urlSchema);

            var ns = prefix.Equals(string.Empty)
                ? xml.Root.GetDefaultNamespace()
                : xml.Root.GetNamespaceOfPrefix(prefix);

            return ns;
        }

        public static HashResult ValidateHash(string xmlString)
        {
            var xml = xmlString.CreateXDocument(_defaultEnconding);
            var xmlNamespace = GetNamespace(xml);
            var reportedHash = xml.Descendants(xmlNamespace + _hash).FirstOrDefault()?.Value ?? string.Empty;
            var contentHash = GenerateHashContent(xml, xmlNamespace);
            return new HashResult(reportedHash, contentHash.CalculateMd5(), contentHash.ConvertToText(_defaultEnconding));
        }

    }
}
