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

namespace ZipStock.Desktop.Pages.Authorization
{
    public class _AuthModel : PageModel
    {
        Verification verification = new Verification();
        string Email { get; set; }

        public _AuthModel()
        {
            Electron.IpcMain.On("async-msg", (data) =>
            {
                Electron.Dialog.ShowMessageBoxAsync(data.ToString());
            });

            Electron.IpcMain.On("verification-email", (data)=>{
                Email = (string)data;
                EmailResponse response = verification.VerificateEmail(Email);
                if(response.StatusCode == System.Net.HttpStatusCode.OK || response.Server == 200)
                {
                    var mainWindow = Electron.WindowManager.BrowserWindows.First();
                    Electron.IpcMain.Send(mainWindow, "code-verify", Email);
                };
            });
        }

        public void OnGet()
        {
        }
    }
}
