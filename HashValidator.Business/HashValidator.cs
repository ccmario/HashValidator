using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HashValidator.Business
{
    public class HashValidator
    {
        private static readonly Encoding _defaultEnconding = Encoding.GetEncoding("iso-8859-1");
        private static string _content;

        private static string GenerateHash(XDocument xml, XNamespace xmlNamespace)
        {
            try
            {
                using (var content = GenerateHashContent(xml, xmlNamespace))
                {
                    var calculatedHash = content.CalculateMd5();
                    return calculatedHash;
                }
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

            memoryStream.Position = 0;
            _content = memoryStream.ConvertToText(_defaultEnconding);
            memoryStream.Position = 0;

            return memoryStream;
        }

        private static XNamespace GetNamespace(XDocument xml)
        {
            var prefix =
                string.IsNullOrEmpty(xml.Root.GetPrefixOfNamespace("http://www.ans.gov.br/padroes/tiss/schemas"))
                    ? string.Empty
                    : xml.Root.GetPrefixOfNamespace("http://www.ans.gov.br/padroes/tiss/schemas");

            var ns = prefix.Equals(string.Empty)
                ? xml.Root.GetDefaultNamespace()
                : xml.Root.GetNamespaceOfPrefix(prefix);

            return ns;
        }

        private static Stream ConvertToStream(string xml)
        {
            if (!xml.TrimStart().StartsWith("<?xml"))
                xml = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>" + xml;

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, _defaultEnconding);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static HashResult ValidateHash(string xmlString)
        {
            var stream = ConvertToStream(xmlString);
            var xml = stream.CreateXDocument();
            var xmlNamespace = GetNamespace(xml);

            string reportedHash;
            try
            {
                reportedHash = xml.Descendants(xmlNamespace + "hash").First().Value;
            }
            catch
            {
                reportedHash = string.Empty;
            }

            var calculatedHash = GenerateHash(xml, xmlNamespace);

            return new HashResult(reportedHash, calculatedHash, _content);
        }

    }
}
