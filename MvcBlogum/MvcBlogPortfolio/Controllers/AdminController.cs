using MvcBlogPortfolio.Models;
using MvcBlogPortfolio.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogPortfolio.Controllers
{
    public class AdminController : Controller
    {
        Myblog myblog = new Myblog();
        dbModelblog db = new dbModelblog();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YazarOnaylari()
        {
            var data = db.Kullanicilar.Where(x => x.Yazar == true && x.Onaylandi == false).ToList();
                return View(data);
        }
        public ActionResult OnayVer(int id)
        {
            Kullanicilar kl = db.Kullanicilar.FirstOrDefault(x => x.KullaniciId == id);
            kl.Onaylandi = true;
            db.SaveChanges();
            return RedirectToAction("YazarOnaylari");
        }
        [Authorize(Roles ="Admin")]
        public ActionResult RolEkle()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult RolEkle(Rol rol)
        {
            Rol roles = new Rol();
            rol.RolId = roles.RolId;
            rol.RolAdi = roles.RolAdi;
            db.Rol.Add(rol);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult YazarlariListele()
        {
            
            myblog.kullanici = db.Kullanicilar.Where(x => x.Yazar == true).ToList();
            return View(myblog);

        }


    }
}