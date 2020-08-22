using HaberPortal;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace HaberPortal.Controllers
{
    [Authorize]
    public class YonetimController : Controller
    {
        // GET: Yonetim
        HaberContext db = new HaberContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SidebarGetir()
        {
            return View();
        }
        public ActionResult NavbarGetir()
        {
            return View();
        }
        public ActionResult HaberEkle()
        {
            ViewBag.TipID = new SelectList(db.Tip, "Id", "Adi");
            ViewBag.KategoriID = new SelectList(db.Kategori, "Id", "Adi");
            return View();
        }
        [HttpPost]
        public ActionResult HaberEkle(Haber haber)
        {
            HttpPostedFileBase Picture = Request.Files["UploadedPhoto"];
            if (!string.Equals(Picture.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(Picture.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(Picture.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(Picture.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(Picture.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(Picture.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Message = "Dosya Formatını yanlış seçtiniz";
                ViewBag.TipID = new SelectList(db.Tip, "Id", "Adi");
                ViewBag.KategoriID = new SelectList(db.Kategori, "Id", "Adi");
                return View();
            }
            else if (ModelState.IsValid)
            {
                byte[] imgBinaryData = new byte[Picture.ContentLength];
                Picture.InputStream.Read(imgBinaryData, 0, Picture.ContentLength);
                haber.Goruntulenme = 0;
                haber.YazarID = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                haber.YayimTarihi = DateTime.Now;
                haber.Resim = ResimBoyutlandır(imgBinaryData, 900);
                haber.KucukResim = ResimBoyutlandır(imgBinaryData, 500);
                db.Haber.Add(haber);
                db.SaveChanges();
                return RedirectToAction("HaberEkle");
            }
            else
            {
                ViewBag.Message = "Haber yüklenemedi";
                ViewBag.TipID = new SelectList(db.Tip, "Id", "Adi");
                ViewBag.KategoriID = new SelectList(db.Kategori, "Id", "Adi");
                return View();
            }

        }
        public ActionResult HaberListele(int? sayfa)
        {
            var haberler = db.Haber.OrderByDescending(x => x.YayimTarihi).ToPagedList(sayfa ?? 1, 20);
            return View(haberler);
        }
        public ActionResult HaberSil(int id)
        {
            db.Haber.Remove(db.Haber.FirstOrDefault(x => x.Id == id));
            db.SaveChanges();
            return RedirectToAction("HaberListele");
        }
        public ActionResult HaberDüzenle(int id)
        {
            Haber secili = db.Haber.FirstOrDefault(x => x.Id == id);
            ViewBag.KategoriID = new SelectList(db.Kategori, "Id", "Adi", secili.KategoriID);
            ViewBag.TipID = new SelectList(db.Tip, "Id", "Adi", secili.TipID);
            //ViewBag.YazarID = new SelectList(db.Yazar, "Id", "Adi", secili.YazarID);
            return View(secili);
        }
        [HttpPost]
        public ActionResult HaberDüzenle(Haber haber)
        {
            Haber bul = db.Haber.FirstOrDefault(x => x.Id == haber.Id);
            if (ModelState.IsValid)
            {
                byte[] imgBinaryData = null;
                HttpPostedFileBase Picture = Request.Files["UploadedPhoto"] ?? null;
                if (Picture.ContentLength != 0)
                {
                    imgBinaryData = new byte[Picture.ContentLength];
                    Picture.InputStream.Read(imgBinaryData, 0, Picture.ContentLength);
                    bul.Resim = ResimBoyutlandır(imgBinaryData, 900);
                    bul.KucukResim = ResimBoyutlandır(imgBinaryData, 500);
                }
                bul.Baslik = haber.Baslik;
                bul.Ozet = haber.Ozet;
                bul.Icerik = haber.Icerik;
                bul.KategoriID = haber.KategoriID;
                bul.TipID = haber.TipID;
                db.SaveChanges();
                return RedirectToAction("HaberListele");
            }
            else
            {
                ViewBag.KategoriID = new SelectList(db.Kategori, "Id", "Adi", bul.KategoriID);
                ViewBag.TipID = new SelectList(db.Tip, "Id", "Adi", bul.TipID);
                //ViewBag.YazarID = new SelectList(db.Yazar, "Id", "Adi", secili.YazarID);
                return View(bul);
            }

        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "UyeGiris");
        }
        byte[] ResimBoyutlandır(byte[] image, int yükseklik)
        {
            WebImage resim = new WebImage(image);
            int destWidth = resim.Width * yükseklik / resim.Height;
            int destHeight = yükseklik;// sourceHeight * Yukseklik / imgPhoto.Width; //resmin bozulmaması için en boy ayarını veriyoruz.
            resim.Resize(destWidth, destHeight, true, true);

            byte[] imgBinaryData = resim.GetBytes();
            return imgBinaryData;
        }
    }
}