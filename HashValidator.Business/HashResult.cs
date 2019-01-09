namespace HashValidator.Business
{
    public class HashResult
    {
        public string ReportedHash { get; private set; }
        public string CalculatedHash { get; private set; }
        public string Content { get; private set; }

        public HashResult(string reportedHash, string calculatedHash, string content)
        {
            ReportedHash = reportedHash.ToUpper();
            CalculatedHash = calculatedHash.ToUpper();
            Content = content;
        }
    }
}
