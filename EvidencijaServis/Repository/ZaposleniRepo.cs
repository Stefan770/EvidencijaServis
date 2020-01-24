using EvidencijaServis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EvidencijaServis.Repository
{
    public class ZaposleniRepo : IDisposable, IZaposleniRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Zaposleni> GetAll()
        {
            return db.Zaposleni.Include(x => x.Kompanija).OrderByDescending(x => x.Plata);
        }

        public IEnumerable<Zaposleni> GetByGodiste(int godiste)
        {
            return db.Zaposleni.Include(x => x.Kompanija).Where(x => x.GodinaRodjenja > godiste).OrderBy(x => x.GodinaRodjenja);
        }

        public IEnumerable<Zaposleni> GetByZaposlenje(ZaposleniFilter filter)
        {
            return db.Zaposleni.Include(x => x.Kompanija)
                .Where(x => x.GodinaZaposlenja >= (filter.Pocetak ?? 0) && x.GodinaZaposlenja <= (filter.Kraj ?? int.MaxValue))
                .OrderBy(x => x.GodinaZaposlenja);
        }

        public Zaposleni GetById(int id)
        {
            return db.Zaposleni.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Zaposleni zaposleni)
        {
            db.Zaposleni.Add(zaposleni);
            db.SaveChanges();
        }

        public void Update(Zaposleni zaposleni)
        {
            db.Entry(zaposleni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Zaposleni zaposleni)
        {
            db.Zaposleni.Remove(zaposleni);
            db.SaveChanges();
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