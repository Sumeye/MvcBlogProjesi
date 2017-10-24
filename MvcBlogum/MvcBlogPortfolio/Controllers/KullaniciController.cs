using MvcBlogPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
namespace MvcBlogPortfolio.Controllers
{
    
    public class KullaniciController : Controller
    {
        dbModelblog db = new dbModelblog();
        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GirisYap()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Kullanicilar kl)
        {
            string ka   = ValidateUser(kl.KullaniciAdi, kl.Parola);
            if (!string.IsNullOrEmpty(ka))//Rol Null veya boş değilse yani Kullanici varsa
            {
                //Role göre kullanıcıya giriş yaptırmamızı sağlar.
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, kl.KullaniciAdi, DateTime.Now, DateTime.Now.AddMinutes(15), true, ka, FormsAuthentication.FormsCookiePath);

                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);
                if (ticket.IsPersistent)
                {
                    ck.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(ck);
                FormsAuthentication.RedirectFromLoginPage(kl.KullaniciAdi, true);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GirisYap");
        }

        string ValidateUser(string ka,string pwd)
        {
            Kullanicilar kl = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == ka && x.Parola == pwd);
            if (kl !=null)
            {
                Session.Add("KullaniciId",kl.KullaniciId);
                Session.Add("KullaniciAdi", kl.KullaniciAdi);
                Session.Add("Sifre", kl.Parola);
                return kl.KullaniciAdi;
            }
            else
            {
                return "";
            }
        }
    

        public ActionResult CikisYap()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult UyeOl()
        {
            return View();

        }
        [HttpPost]
        public ActionResult UyeOl(Kullanicilar kl,string rdBay, string rdBayan)
        {
            if (!string.IsNullOrEmpty(rdBay))
                kl.Cinsiyet = true;
            if (!string.IsNullOrEmpty(rdBayan))
                kl.Cinsiyet = false;
            kl.Yazar = false;
            kl.Onaylandi = true;
            kl.Aktif = true;

            kl.KayitTarihi = DateTime.Now;
            db.Kullanicilar.Add(kl);
            db.SaveChanges();
            return RedirectToAction("GirisYap");
        }

    }
}