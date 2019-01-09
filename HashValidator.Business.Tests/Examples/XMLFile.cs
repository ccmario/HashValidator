using System.IO;
using System.Reflection;
using System.Text;

namespace HashValidator.Business.Tests.Examples
{
    public class XMLFile
    {
        public string Name { get; private set; }
        public string Hash { get; private set; }
        public string Content { get; private set; }

        public XMLFile(string fileName)
        {
            Name = fileName;
            Content = GetResourceFileContentAsString(fileName);
            Hash = Content.Substring(Content.IndexOf("hash>") + 5, 32);
        }

        private string GetResourceFileContentAsString(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "HashValidator.Business.Tests.Examples." + fileName;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
