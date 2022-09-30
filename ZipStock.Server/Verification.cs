using System;
using System.Net;

namespace ZipStock.Server
{
    public class Verification
    {
        private string _email { get; set; }

        public Verification(string email)
        {
            _email = email;
        }

        public void VerificateEmail()
        {
            string response = HttpGet($"http://zipstock.click/api/code-verification.php?email={_email}");
        }

        public string HttpGet(string uri)
        {
            string content = null;

            var wc = new WebClient();
            content = wc.DownloadString(uri);

            return content;
        }
    }
}

