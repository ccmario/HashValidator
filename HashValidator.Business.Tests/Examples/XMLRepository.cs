using System.Collections.Generic;

namespace HashValidator.Business.Tests.Examples
{
    public class XMLRepository
    {
        private List<XMLFile> _list;
        private const int numberOfResourceFiles = 7;

        public XMLRepository()
        {
            _list = new List<XMLFile>();
            for (int i = 1; i <= numberOfResourceFiles; i++)
            {
                _list.Add(new XMLFile($"XMLFile{i:00}.xml"));
            }
        }

        public IEnumerable<XMLFile> GetXMLFiles()
        {
            return _list;
        }
    }
}
