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

        private static HashResult GenerateHashResult(XDocument xml, XNamespace xmlNamespace)
        {
            try
            {
                var reportedHash = xml.Descendants(xmlNamespace + "hash").FirstOrDefault()?.Value ?? string.Empty;
                var contentHash = GenerateHashContent(xml, xmlNamespace);
                return new HashResult(reportedHash, contentHash.CalculateMd5(), contentHash.ConvertToText(_defaultEnconding));
            }
            finally
            {
                xml.RemoveNodes();
            }
        }

        private static Stream GenerateHashContent(XDocument xml, XNamespace ns)
        {
            xml.Root.Descendants(ns + "epilogo").Remove();
            xml.Root.Descendants(ns + "hash").Remove();

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
            return GenerateHashResult(xml, GetNamespace(xml));
        }

    }
}
