namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class Operator
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string? officeNumber { get; set; }
        public string? identityProvider { get; set; }
        public string[]? roles { get; set; }
        public string? title { get; set; }
        public Error[]? errors { get; set; }

    }
}
