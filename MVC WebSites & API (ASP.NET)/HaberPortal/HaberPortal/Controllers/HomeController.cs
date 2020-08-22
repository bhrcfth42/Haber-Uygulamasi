using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace HaberPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        HaberContext db = new HaberContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SliderGetir()
        {
            var haberler = db.Haber.Where(x => x.Tip.Adi == "Manşet").OrderByDescending(x => x.YayimTarihi).Take(10);
            return View(haberler);
        }
        public ActionResult EnSonHaberler()
        {
            var haberler = db.Haber.OrderByDescending(x => x.YayimTarihi).Take(12);
            return View(haberler);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Portfolio(int? sayfa)
        {
            var haberler = db.Haber.OrderByDescending(x => x.YayimTarihi).ToPagedList(sayfa ?? 1, 10);
            return View(haberler);
        }
        public ActionResult Contect()
        {
            return View();
        }
        public ActionResult Navigation()
        {
            return View();
        }
    }
}