using coursework_kpiyap.Models;
using coursework_kpiyap.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coursework_kpiyap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServiceService _serviceService;

        public ServicesController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public ActionResult<List<Service>> Get() =>
            _serviceService.Get();

        [HttpGet("{id:length(24)}", Name = "GetService")]
        public ActionResult<Service> Get(string id)
         {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        [HttpPost]
        public ActionResult<Service> Create(Service service)
        {
            _serviceService.Create(service);
            return CreatedAtRoute("GetService", new { id = service._id.ToString() }, service);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Service serviceIn)
        {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            _serviceService.Update(id, serviceIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            _serviceService.Remove(service._id);

            return NoContent();
        }
    }

}
