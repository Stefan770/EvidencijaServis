using System.Collections.Generic;
using EvidencijaServis.Models;

namespace EvidencijaServis.Repository
{
    public interface IKompanijaRepo
    {
        IEnumerable<Kompanija> GetAll();
        Kompanija GetById(int id);
        IEnumerable<KompanijaStatDTO> GetStatistika();
        IEnumerable<Kompanija> GetTradicija();
    }
}