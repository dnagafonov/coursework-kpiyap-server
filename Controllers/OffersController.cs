using coursework_kpiyap.Models;
using coursework_kpiyap.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace coursework_kpiyap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly OfferService _offerService;

        public OffersController(OfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        public ActionResult<Offer> Create([FromBody]Offer offer)
        {
            return _offerService.Create(offer);
        }

    }

}
