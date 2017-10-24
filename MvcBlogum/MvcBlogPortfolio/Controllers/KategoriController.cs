using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogPortfolio.Controllers
{
    using Models;
    
    public class KategoriController : Controller
    {
        dbModelblog db = new dbModelblog();
        // GET: Kategori
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public PartialViewResult KategoriListele()
        {
            return PartialView(db.Kategori.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.KategoriID == id).ToList();
            return View("MakaleListele", data);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kat)
        {
            Kategori kt = new Kategori();
            kt.KategoriAdi = kat.KategoriAdi;
            kt.KategoriId = kat.KategoriId;
            db.Kategori.Add(kt);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}