using Xunit;

namespace HashValidator.Business.Tests
{
    public class HashValidatorTest
    {
        [Fact]
        public void When_Informed_A_Valid_XML_File_Must_Return_A_Valid_Hash()
        {
            var repository = new XMLRepository();
            foreach (var file in repository.GetXMLFiles())
            {
                var result = HashValidator.ValidateHash(file.Content);
                Assert.True(result.CalculatedHash.ToUpper() == result.ReportedHash.ToUpper(), 
                    $"File: {file.Name}. Reported hash: {result.ReportedHash}. Calculated hash: {result.CalculatedHash}.");
            }
        }
    }
}
