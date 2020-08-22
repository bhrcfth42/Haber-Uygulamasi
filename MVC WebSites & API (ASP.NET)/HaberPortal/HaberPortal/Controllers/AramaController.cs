using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberPortal.Controllers
{
    public class AramaController : Controller
    {
        HaberContext db = new HaberContext();
        // GET: Arama
        public ActionResult Ara(string txtAra, int? sayfa)
        {
            var haberler = db.Haber.Where(x => x.Baslik.Contains(txtAra) || x.Ozet.Contains(txtAra) || x.Icerik.Contains(txtAra)).OrderByDescending(x => x.YayimTarihi).ToPagedList(sayfa ?? 1, 20);
            if (txtAra == ""||txtAra==null)
            {
                ViewBag.Message = "Arama Değeri Girmediniz";
                return View();
            }

            else if (haberler.Count != 0)
            {
                ViewBag.Ara = txtAra.ToUpper();
                return View(haberler);
            }
            else
            {
                ViewBag.Message = "Arama Sonucu Bulunamadı";
                return View();
            }

        }
        public ActionResult Navigation()
        {
            return View();
        }
        public ActionResult Contect()
        {
            return View();
        }
        [HttpPost]
        public ActionResult haberler()
        {
            return RedirectToAction("Portfolio", "Home");
        }
    }
}