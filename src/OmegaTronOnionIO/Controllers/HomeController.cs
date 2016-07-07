using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text;

namespace OmegaTronOnionIO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string path)
        {
            path = GetValidPath(path);
            var fileListing = RetrieveFileListing(path);
            ViewBag.Path = path;

            return View(fileListing);
        }

        public Models.FileListing RetrieveFileListing(string path)
        {
            NameValueCollection headers = new NameValueCollection();
            headers.Add("X-API-KEY", "<YOUR APIKEY HERE>");
            Models.FileListing fileListing = Helpers.JsonHelper.SendToServer<Models.FileListing>("https://api.onion.io/v1/devices/<YOUR DEVICEID HERE>/file/list", headers, "{\"path\":\"" + path + "\"}", "POST");
            return fileListing;
        }

        public string GetValidPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }
            return path;
        }

    }
}
