using HashValidator.Business.Tests.Examples;
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
                var calculatedHash = HashValidator.ValidateHash(file.Content).CalculatedHash;
                Assert.True(calculatedHash.ToUpper() == file.Hash.ToUpper(), $"File: {file.Name}. Reported hash: {file.Hash}. Calculated hash: {calculatedHash}.");
            }
        }
    }
}
