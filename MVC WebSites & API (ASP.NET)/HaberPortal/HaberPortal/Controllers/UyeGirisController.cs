using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HaberPortal.Controllers
{
    public class UyeGirisController : Controller
    {
        // GET: UyeGiris
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string KullaniciAdi, string Parola, string ReturnUrl="")
        {
            if (Membership.ValidateUser(KullaniciAdi, Parola))
            {
                FormsAuthentication.RedirectFromLoginPage(KullaniciAdi,false);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Yonetim");
                }
                
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Parola Hatalı";
            }
            return View();
        }
        //public ActionResult Ekle()
        //{
        //    MembershipCreateStatus durum;
        //    Membership.CreateUser("admin", "admin1234", "fatihbuhurcu539@gmail.com", "aa", "aa", true, out durum);
        //    return View();
        //}
    }
}