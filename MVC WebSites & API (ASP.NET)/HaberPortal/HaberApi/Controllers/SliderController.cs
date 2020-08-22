using HaberApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaberPortalApi.Controllers
{
    public class SliderController : ApiController
    {
        HaberContext db = new HaberContext();
        public IEnumerable<Haber> GetHabers()
        {
            var haberler = db.Haber.Where(x=>x.Tip.Adi=="Manşet").OrderByDescending(x => x.YayimTarihi).Take(10);
            return haberler;
        }
    }
}
