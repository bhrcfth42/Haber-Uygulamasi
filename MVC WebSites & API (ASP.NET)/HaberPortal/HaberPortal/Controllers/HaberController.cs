using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberPortal.Controllers
{
    public class HaberController : Controller
    {
        // GET: Haber
        HaberContext db = new HaberContext();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contect()
        {
            return View();
        }
        public ActionResult Navigation()
        {
            return View();
        }
        public ActionResult Goster(int id)
        {
            var secilen = db.Haber.FirstOrDefault(x => x.Id == id);
            secilen.Goruntulenme++;
            db.SaveChanges();
            return View(secilen);
        }
    }
}