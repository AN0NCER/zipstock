using System;
using System.IO;

namespace ZipStock.Desktop
{
    static class IO
    {
        private static string _fileName = "acc.json";
        private static string _account;
        public static string Account
        {
            get
            {
                if(_account == null)
                {
                    _account = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);
                }
                return _account;
            }
            set
            {
                return;
            }
        }
    }

    static class Global
    {
        public static ElectronNET.API.BrowserWindow browserWindow {get;set;}
        public static Account globalAccount { get; set; }
    }

    public class Account
    {
        public string Oauth { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

