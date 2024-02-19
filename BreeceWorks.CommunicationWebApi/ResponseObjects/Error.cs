using BreeceWorks.CommunicationWebApi.Objects;
using System;

namespace BreeceWorks.CommunicationWebApi.ResponseObjects
{
    public class Error
    {
        public String code { get; set; }
        public String category { get; set; }
        public Boolean retryable { get; set; }

        public Int32 status { get; set; }
        public String detail { get; set; }
        public ErrorRspseMeta? meta { get; set; }
        public String  path { get; set; }
        public String method { get; set; }
        public Guid requestId { get; set; }
    }
}
