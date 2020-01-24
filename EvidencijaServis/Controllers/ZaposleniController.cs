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
    public class ZaposleniController : ApiController
    {
        IZaposleniRepo _repository { get; set; }

        public ZaposleniController(IZaposleniRepo repo)
        {
            _repository = repo;
        }

        public IEnumerable<Zaposleni> Get()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Zaposleni> GetByGodiste(int godiste)
        {
            return _repository.GetByGodiste(godiste);
        }

        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult Get(int id)
        {
            var zaposleni = _repository.GetById(id);
            if (zaposleni == null)
            {
                return NotFound();
            }
            return Ok(zaposleni);
        }

        [Authorize]
        [Route("api/zaposlenje")]
        public IEnumerable<Zaposleni> PostFiltered(ZaposleniFilter filter)
        {
            return _repository.GetByZaposlenje(filter);
        }

        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult Post(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(zaposleni);
            return CreatedAtRoute("DefaultApi", new { id = zaposleni.Id }, zaposleni);
        }

        [Authorize]
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult Put(int id, Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaposleni.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(zaposleni);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(zaposleni);
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            var zaposleni = _repository.GetById(id);
            if (zaposleni == null)
            {
                return NotFound();
            }

            _repository.Delete(zaposleni);
            return Ok();
        }
    }
}
