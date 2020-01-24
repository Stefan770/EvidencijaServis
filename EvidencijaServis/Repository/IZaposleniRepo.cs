using System.Collections.Generic;
using EvidencijaServis.Models;

namespace EvidencijaServis.Repository
{
    public interface IZaposleniRepo
    {
        void Add(Zaposleni zaposleni);
        void Delete(Zaposleni zaposleni);
        IEnumerable<Zaposleni> GetAll();
        IEnumerable<Zaposleni> GetByZaposlenje(ZaposleniFilter filter);
        IEnumerable<Zaposleni> GetByGodiste(int godiste);
        Zaposleni GetById(int id);
        void Update(Zaposleni zaposleni);
    }
}