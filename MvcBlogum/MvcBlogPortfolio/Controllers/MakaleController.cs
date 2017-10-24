using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBlogPortfolio.App_Classes;

namespace MvcBlogPortfolio.Controllers
{
    using Models;
    using MvcBlogPortfolio.Tools;
    using System.Drawing;
    using System.IO;
    using System.Web.Helpers;

    [Authorize]
    public class MakaleController : Controller
    {
        Myblog myblog = new Myblog();
        dbModelblog db = new dbModelblog();
        Resim rsm = new Resim();
        // GET: Makale
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = db.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(data);
        }
    
  
        [AllowAnonymous]
        public string Begen(int id)
        {
            Makale mkl = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
            mkl.Begeni++;//Begeni +1
            db.SaveChanges();
            return mkl.Begeni.ToString();
        }
        //[AllowAnonymous]
        public string Goruntulendi(int id)
        {
            Makale mkl = db.Makale.FirstOrDefault(x => x.MakaleId == id);
            mkl.GoruntulenmeSayisi++;
            db.SaveChanges();
            return mkl.GoruntulenmeSayisi.ToString();
        }


        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult Ekle()
        {
            ViewBag.Kategoriler = db.Kategori.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Yazar")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Ekle(Makale mkl, HttpPostedFileBase resim)
        {
            try
            {
                Image img = Image.FromStream(resim.InputStream);

                Bitmap kckResim = new Bitmap(img, Setttings.ResimKucukBoyut);
                Bitmap ortaResim = new Bitmap(img, Setttings.ResimOrtaBoyut);
                Bitmap bykResim = new Bitmap(img, Setttings.ResimBuyukBoyut);
                bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));
                ortaResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
                kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName));

                Resim rsm = new Resim();

                rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;
                rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
                rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;
                db.Resim.Add(rsm);
                db.SaveChanges();
                mkl.ResimID = rsm.ResimId;
                mkl.EklenmeTarihi = DateTime.Now;
                mkl.GoruntulenmeSayisi = 0;
                mkl.Begeni = 0;


                int yzrId = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
                mkl.KullaniciID = yzrId;
                db.Makale.Add(mkl);
                db.SaveChanges(); 

            }
            catch (Exception )
            {

               
            }

            return RedirectToAction("Index", "Home");
        }


        //[Authorize(Roles ="Admin,Yazar")]
        //public ActionResult Guncelle(int? id)
        //{
        //    myblog.makale = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
        //     myblog.kategori= db.Kategori.ToList();
        //    return View(myblog);
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //[Authorize(Roles ="Admin,Yazar")]
        //public ActionResult Guncelle(Makale mkl,HttpPostedFileBase img,int? id)
        //{
        //    Makale guncelle = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
        //        guncelle.KategoriID = mkl.KategoriID;
        //        guncelle.Baslik = mkl.Baslik;
        //        guncelle.Icerik = mkl.Icerik;
        //    guncelle.EklenmeTarihi = DateTime.Now;

        //    db.SaveChanges();
        //    return RedirectToAction("YazarınMakaleleri","Yazar");
        //        //return new RedirectResult("~/Home/Index/" + guncelle.MakaleId);
        //    }

        //public ActionResult Sil(int? id)
        //{

        //    Yorum sil = db.Yorum.Where(x => x.MakaleID == id).FirstOrDefault();
        //    if (sil != null) { 
        //     db.Yorum.Remove(sil);
        //    }

        //    Makale silinen = db.Makale.Where(x => x.MakaleId == id).FirstOrDefault();
        //    db.Makale.Remove(silinen);
        //    db.SaveChanges();
        //    return RedirectToAction("Index","Home");  
        //}

     
        [Authorize(Roles ="Admin,Yazar")]
        public ActionResult YorumYaz(Yorum yrm,int? id)
        {

            Yorum mkl = db.Yorum.Where(x => x.MakaleID == id).FirstOrDefault();
            Yorum yorum = new Yorum();
            yorum.MakaleID = id;
           
            yorum.AdSoyad = yrm.AdSoyad;
            yorum.YorumIcerigi = yrm.YorumIcerigi;
            yorum.EklenmeTarihi = DateTime.Now;
            
            db.Yorum.Add(yorum);
            db.SaveChanges();
            return new RedirectResult("~/Makale/Detay/" + yorum.MakaleID);

        }


    }
}