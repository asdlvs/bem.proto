using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dnevnik.Mvc.Controllers
{
    using Dnevnik.Mvc.Models;

    public class HomeController : Controller
    {
        private readonly List<Main> _models = new List<Main>(); 

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Main model)
        {
            this._models.Add(model);
            return View(model);
        }
    }
}