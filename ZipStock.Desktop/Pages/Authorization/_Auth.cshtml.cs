using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using ZipStock.Server;
using Newtonsoft.Json;
using System.IO;

namespace ZipStock.Desktop.Pages.Authorization
{
    public class _AuthModel : PageModel
    {
        Verification verification = new Verification();
        string Email { get; set; }
        string Code { get; set; }

        public _AuthModel()
        {
            Electron.IpcMain.On("async-msg", (data) =>
            {
                Electron.Dialog.ShowMessageBoxAsync(data.ToString());
            });

            Electron.IpcMain.On("verification-email", (data) =>
            {
                Email = (string)data;
                EmailResponse response = verification.VerificateEmail(Email);
                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Server == 200)
                {
                    var mainWindow = Electron.WindowManager.BrowserWindows.First();
                    Electron.IpcMain.Send(mainWindow, "code-verify", Email);
                };
            });

            Electron.IpcMain.On("verification-code", async (data) =>
            {
                Code = (string)data;
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow, "loading-code");
                CodeResponse response = verification.VerificateCode(Code, Email);
                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Server == 200)
                {
                    SaveOAuth(response.Oauth);
                    Electron.IpcMain.Send(mainWindow, "logged-account");
                }else
                {
                    await Electron.Dialog.ShowMessageBoxAsync($"Error: {response.ErrorMessage}");
                    Electron.IpcMain.Send(mainWindow, "loading-code");
                }
            });
        }

        public void SaveOAuth(string Oauth)
        {
            Account account = new Account();
            account.Oauth = Oauth;
            System.IO.File.WriteAllText(IO.Account, JsonConvert.SerializeObject(account));
        }

        public void OnGet()
        {
        }
    }
}
