using MvcBlogPortfolio.App_Classes;
using MvcBlogPortfolio.Models;
using MvcBlogPortfolio.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcBlogPortfolio.Controllers
{
    public class YazarController : Controller
    {
        Myblog myblog = new Myblog();
        dbModelblog db = new dbModelblog();
        Resim rsm = new Resim();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YazarOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarOl(Kullanicilar kl,string rdBay,string rdBayan)
        {
            if (!string.IsNullOrEmpty(rdBay))
                kl.Cinsiyet = true;
            if(!string.IsNullOrEmpty(rdBayan))
                kl.Cinsiyet = false;
            kl.Yazar = true;
            kl.Onaylandi = false;
            kl.Aktif = true;
            kl.KayitTarihi = DateTime.Now;
            db.Kullanicilar.Add(kl);
            db.SaveChanges();

            Rol yazar = db.Rol.FirstOrDefault(x => x.RolAdi == "Yazar");
            KullaniciRol kr = new KullaniciRol();
            kr.RolID = yazar.RolId;
            kr.KullaniciID = kl.KullaniciId;
            db.KullaniciRol.Add(kr);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles ="Yazar")]
        public ActionResult YazarınMakaleleri()
        {
            int id = Convert.ToInt32(Session["KullaniciId"]); 

                myblog.makaleler = db.Makale.Where(x => x.KullaniciID ==id).ToList();
               return View(myblog);

        }
  
        public ActionResult Sil(int id)
        {
            Kullanicilar silinen = db.Kullanicilar.Where(x => x.KullaniciId == id).FirstOrDefault();
            db.Kullanicilar.Remove(silinen);
            db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult Guncelle(int? id)
        {
            myblog.makale = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
            myblog.kategori = db.Kategori.ToList();
            return View(myblog);
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult Guncelle(Makale mkl, HttpPostedFileBase img, int? id)
        {
            Makale guncelle = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
            guncelle.KategoriID = mkl.KategoriID;
            guncelle.Baslik = mkl.Baslik;
            guncelle.Icerik = mkl.Icerik;
            guncelle.EklenmeTarihi = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("YazarınMakaleleri", "Yazar");
            //return new RedirectResult("~/Home/Index/" + guncelle.MakaleId);
        }

        public ActionResult MakaleSil(int? id)
        {

            Yorum sil = db.Yorum.Where(x => x.MakaleID == id).FirstOrDefault();
            Etiket etiket = db.Etiket.Where(x => x.EtiketId == id).FirstOrDefault();
            if (sil != null)
            {
                db.Yorum.Remove(sil);
             
            }
            Makale silinen = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
            db.Makale.Remove(silinen);
            db.SaveChanges();
            return RedirectToAction("YazarınMakaleleri", "Yazar");
        }

    }
}