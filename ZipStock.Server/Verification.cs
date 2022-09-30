using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ZipStock.Server
{
    public class Verification
    {
        public EmailResponse VerificateEmail(string email)
        {
            IResponse response = WebGet($"http://zipstock.click/api/email-verification.php?email={email}");
            EmailResponse eResponse = JsonConvert.DeserializeObject<EmailResponse>(response.Data);
            eResponse.Data = response.Data;
            eResponse.StatusCode = response.StatusCode;
            eResponse.Method = response.Method;
            return eResponse;
        }

        public CodeResponse VerificateCode(string code, string email)
        {
            IResponse response = WebGet($"https://zipstock.click/api/code-verification.php?email={email}&code={code}");
            CodeResponse cResponse = JsonConvert.DeserializeObject<CodeResponse>(response.Data);
            cResponse.Data = response.Data;
            cResponse.StatusCode = response.StatusCode;
            cResponse.Method = response.Method;
            return cResponse;
        }

        public IResponse WebGet(string url)
        {
            IResponse response = new IResponse();
            var request = WebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(webStream))
                    {
                        string data = reader.ReadToEnd();
                        response.Data = data;
                    }
                }
                response.StatusCode = webResponse.StatusCode;
            }
            response.Method = "GET";
            return response;
        }
    }

    public class EmailResponse : IResponse
    {
        [JsonProperty("server")]
        public long Server { get; set; }

        [JsonProperty("new_user")]
        public bool NewUser { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class CodeResponse : IResponse
    {
        [JsonProperty("server")]
        public long Server { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("oauth")]
        public string Oauth { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}

