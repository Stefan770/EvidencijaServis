using EvidencijaServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaServis.Repository
{
    public class KompanijaRepo : IDisposable, IKompanijaRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Kompanija> GetAll()
        {
            return db.Kompanije;
        }

        public Kompanija GetById(int id)
        {
            return db.Kompanije.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Kompanija> GetTradicija()
        {
            return new List<Kompanija>
            {
                db.Kompanije.OrderBy(x => x.Godina).FirstOrDefault(),
                db.Kompanije.OrderByDescending(x => x.Godina).FirstOrDefault()
            };
        }

        public IEnumerable<KompanijaStatDTO> GetStatistika()
        {
            return db.Kompanije
                .Select(x => new KompanijaStatDTO { Naziv = x.Naziv, ProsecnaPlata = db.Zaposleni.Where(z => z.KompanijaId == x.Id).Average(z => z.Plata) })
                .OrderByDescending(x => x.ProsecnaPlata);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}