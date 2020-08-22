using HaberApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaberPortalApi.Controllers
{
    public class EnSonHaberController : ApiController
    {
        HaberContext db = new HaberContext();
        public IEnumerable<Haber> GetHabers()
        {
            var haberler = db.Haber.OrderByDescending(x => x.YayimTarihi).Take(12);
            return haberler;
        }
    }
}
