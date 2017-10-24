using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogPortfolio.Controllers
{
    using Models;
    public class HomeController : Controller
    {
        dbModelblog db = new dbModelblog();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakaleListele()
        {
            var data = db.Makale.OrderByDescending(x => x.EklenmeTarihi).ToList();
            return View("MakaleListeleWidget", data);
        }
        public PartialViewResult PopulerMakalelerWidget()
        {
            var model = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Take(3).ToList();
            return PartialView(model);
        }
    }
}