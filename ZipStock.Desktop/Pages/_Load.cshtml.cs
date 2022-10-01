using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElectronNET.API;
using Microsoft.AspNetCore.Components;
using System.Threading;
using System.IO;
using System.Linq;

namespace ZipStock.Desktop.Pages
{
    public class _Load : PageModel
    {
        public _Load()
        {
            Electron.IpcMain.On("load-account", (data) =>
            {
                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                if (System.IO.File.Exists(IO.Account))
                {
                    Electron.IpcMain.Send(mainWindow, "rediret-to", "/zip");
                }
                else
                {
                    Electron.IpcMain.Send(mainWindow, "rediret-to", "/auth");
                }
            });
        }

        public void OnGet()
        {
        }
    }
}

