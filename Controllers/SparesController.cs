using coursework_kpiyap.Models;
using coursework_kpiyap.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coursework_kpiyap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparesController : ControllerBase
    {
        private readonly SpareService _spareService;

        public SparesController(SpareService spareService)
        {
            _spareService = spareService;
        }

        [HttpGet]
        public ActionResult<List<Service>> Get() =>
            _spareService.Get();
        [EnableCors("Policy1")]
        [HttpGet("{id:length(24)}", Name = "GetSpare")]
        public ActionResult<Service> Get(string id)
        {
            var service = _spareService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        [HttpPost]
        public ActionResult<Service> Create(Service service)
        {
            _spareService.Create(service);
            return CreatedAtRoute("GetSpare", new { id = service._id.ToString() }, service);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Service serviceIn)
        {
            var service = _spareService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            _spareService.Update(id, serviceIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var service = _spareService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            _spareService.Remove(service._id);

            return NoContent();
        }
    }

}
