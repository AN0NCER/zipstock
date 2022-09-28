using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElectronNET.API;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace ZipStock.Desktop.Pages
{
    public class _Load : PageModel
    {
        public _Load()
        {
            //Global.browserWindow.LoadURL("https://zipstock.click");
        }

        public void OnGet()
        {
            Thread.Sleep(1000);
        }
    }
}

