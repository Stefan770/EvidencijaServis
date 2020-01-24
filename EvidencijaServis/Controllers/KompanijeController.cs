using EvidencijaServis.Models;
using EvidencijaServis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EvidencijaServis.Controllers
{
    public class KompanijeController : ApiController
    {
        IKompanijaRepo _repository { get; set; }

        public KompanijeController(IKompanijaRepo repo)
        {
            _repository = repo;
        }

        public IEnumerable<Kompanija> Get()
        {
            return _repository.GetAll();
        }

        [Route("api/tradicija")]
        public IEnumerable<Kompanija> GetTradicija()
        {
            return _repository.GetTradicija();
        }

        [Route("api/statistika")]
        public IEnumerable<KompanijaStatDTO> GetStatistika()
        {
            return _repository.GetStatistika();
        }

        [ResponseType(typeof(Kompanija))]
        public IHttpActionResult Get(int id)
        {
            var state = _repository.GetById(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
