using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace HashValidator.Business
{
    public static class StreamExtension
    {
        public static string ConvertToText(this Stream input, Encoding encoding)
        {
            StreamReader reader = new StreamReader(input, encoding);
            return reader.ReadToEnd();
        }

        public static string CalculateMd5(this Stream stream)
        {
            stream.Position = 0;
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);

            var sb = new StringBuilder();
            foreach (var i in hash)
                sb.Append(i.ToString("x2"));

            Array.Clear(hash, 0, hash.Length);
            return sb.ToString();
        }

        public static XDocument CreateXDocument(this Stream xmlFile)
        {
            xmlFile.Position = 0;
            return XDocument.Load(xmlFile, LoadOptions.PreserveWhitespace);
        }
    }
}
