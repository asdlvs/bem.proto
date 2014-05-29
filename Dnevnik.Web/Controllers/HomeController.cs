using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dnevnik.Web.Controllers
{
    using Dnevnik.Blocks.Models;

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var widgets = new List<Widget>
                  {
                      new Widget
                      {
                          Title = "First",
                          Content = "First content"
                      },
                      new Widget
                      {
                          Title = "Second",
                          Content = "second content"
                      },
                      new Widget
                      {
                          Title = "Third",
                          Content = "third content"
                      }
                  };
            return this.View(widgets);
        }

        [HttpPost]
        public ActionResult Index(List<Widget> widgets)
        {
            return this.View(widgets);
        }

        public ActionResult Index2()
        {
            return View();
        }
    }
}