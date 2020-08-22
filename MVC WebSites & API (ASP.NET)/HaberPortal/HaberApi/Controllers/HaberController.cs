using HaberApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace HaberPortalApi.Controllers
{
    public class HaberController : ApiController
    {
        HaberContext db = new HaberContext();
        public IEnumerable<Haber> GetHabers()
        {
            var haberler = db.Haber.OrderByDescending(x => x.YayimTarihi);
            return haberler;
        }
        public Haber GetHabers(int id)
        {
            var haber = db.Haber.Find(id);
            return haber;
        }
        public IHttpActionResult PostHabers(int id)
        {
            var secilen = db.Haber.FirstOrDefault(x => x.Id == id);
            secilen.Goruntulenme++;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.OK);
        }
    }
}
