namespace BreeceWorks.CommunicationHub
{
    public class CommunicationResponse
    {
        public int StatusCode { get; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; }

        public CommunicationResponse(int statusCode, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            StatusCode = statusCode;
            Headers = headers;
        }
    }



    public class CommunicationResponse<TResult> : CommunicationResponse
    {
        public TResult Result { get; }

        public CommunicationResponse(int statusCode, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result)
            : base(statusCode, headers)
        {
            Result = result;
        }
    }

    public class CommunicationException : System.Exception
    {
        public int StatusCode { get; }

        public string Response { get; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; }

        public CommunicationException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }



        public override string ToString()
        {
            return $"HTTP Response: \n\n{Response}\n\n{base.ToString()}";
        }
    }
}
