using System;

namespace ClientMaster
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        //public int ClientTypeId { get; set; }
        //public string EmailAddress { get; set; }
        public string CountryCode { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string Locale { get; set; }
        public string ZoneInfo { get; set; }
        public string ResponseMessage { get; set; }
    }
}
