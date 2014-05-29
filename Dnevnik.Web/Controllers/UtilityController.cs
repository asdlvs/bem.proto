using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dnevnik.Web.Controllers
{
    using System.Reflection;

    public class UtilityController : Controller
    {
        private readonly Assembly _assembly;
        private readonly string[] _resources;

        public UtilityController()
        {
            _assembly = typeof(Dnevnik.Blocks.Marker).Assembly;
            _resources = _assembly.GetManifestResourceNames();

        }

        // GET: Utility
        public ActionResult File(string id)
        {
            var extMarker = id.LastIndexOf('.');
            var name = "DNEVNIK.BLOCKS." + id.Replace('/', '.').ToUpper();
            var ext = id.Substring(extMarker + 1);

            var resourceName = _resources.FirstOrDefault(r => r.ToUpper() == name);
            if (string.IsNullOrWhiteSpace(resourceName)) { return null; }
            byte[] file;

            using (var stream = _assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) { return null; }
                file = new byte[stream.Length];
                stream.Read(file, 0, file.Length);
            }

            string mime;
            switch (ext)
            {
                case "js":
                    mime = "application/javascript";
                    break;
                case "css":
                    mime = "text/css";
                    break;
                default:
                    mime = "text/html";
                    break;
            }

            return new FileContentResult(file, mime);
        }
    }
}