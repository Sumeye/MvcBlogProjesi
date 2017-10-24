using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogPortfolio.Controllers
{
    using Models;
    public class EtiketController : Controller
    {
        dbModelblog db = new dbModelblog();
        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public PartialViewResult EtiketleriListele()
        {
            return PartialView(db.Etiket.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.Etiket.Any(y => y.EtiketId == id)).ToList();
            return View("MakaleListele", data);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult EtiketEkle()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult EtiketEkle(Etiket etiket)
        {
            Etiket etk = new Etiket();
            etk.EtiketAdi = etiket.EtiketAdi;
            etk.EtiketId = etiket.EtiketId;
            db.Etiket.Add(etk);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}