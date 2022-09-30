using System;
using System.Net;
using Newtonsoft.Json;

namespace ZipStock.Server
{
    public class IResponse
    {
        [JsonIgnore]
        public string Method { get; set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public string Data { get; set; }
    }
}

