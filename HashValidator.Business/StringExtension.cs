using System.IO;
using System.Text;
using System.Xml.Linq;

namespace HashValidator.Business
{
    public static class StringExtension
    {
        private static readonly string _xmlHeader = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>";

        public static Stream ConvertToStream(this string xml, Encoding encoding)
        {
            if (!xml.TrimStart().StartsWith("<?xml"))
            {
                xml = string.Concat(_xmlHeader, xml);
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, encoding);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static XDocument CreateXDocument(this string xmlFile, Encoding encoding)
        {
            return xmlFile.ConvertToStream(encoding).CreateXDocument();
        }

    }
}
