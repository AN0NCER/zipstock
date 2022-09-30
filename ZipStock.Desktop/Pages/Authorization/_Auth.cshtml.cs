using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;

namespace ZipStock.Desktop.Pages.Authorization
{
    public class _AuthModel : PageModel
    {
        public _AuthModel()
        {
            Electron.IpcMain.On("async-msg", (data) =>
            {
                Electron.Dialog.ShowMessageBoxAsync(data.ToString());
            });

            Electron.IpcMain.On("verification-email", (data)=>{
                string email = (string)data;
            });
        }

        public void OnGet()
        {
        }
    }
}
