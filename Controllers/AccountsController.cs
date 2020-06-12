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
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public JsonResult Login([FromBody]LoginCreds loginCreds)
        {
            var account = _accountService.Find(loginCreds.Username, loginCreds.Password);
            if (account != null)
                return new JsonResult(new {
                    status = 200,
                    account = new { 
                        _id = account._id,
                        username = account.username,
                        email = account.email,
                        currency = account.currency,
                        cart = account.cart
                    }
                });
            else 
                return new JsonResult(new { status = 404 });
        }

        [HttpPost("cart/add")]
        public JsonResult AddToCart([FromBody]CartCredsAdd cardCreds)
        {
            return _accountService.AddToCart(cardCreds.Id, cardCreds.service);
        }

        [HttpPost("cart/delete")]
        public JsonResult DeleteFromCart([FromBody]CartCredsDelete cardCreds)
        {
            return _accountService.DeleteFromCart(cardCreds.Id, cardCreds.service);
        }

        [HttpPost("cart/drop")]
        public JsonResult DropCart([FromBody]DropCartCreds creds)
        {
            return _accountService.DropCart(creds.id);
        }

        [HttpPost("register")]
        public JsonResult Login([FromBody]Account account)
        {
            return _accountService.Create(account);
        }

        [HttpGet("{id:length(24)}", Name = "GetAccount")]
        public ActionResult<Account> Get(string id)
        {
            var service = _accountService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        [HttpPost]
        public ActionResult<Service> Create(Account account)
        {
            _accountService.Create(account);
            return CreatedAtRoute("GetAccount", new { id = account._id.ToString() }, account);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Account accountIn)
        {
            var account = _accountService.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            _accountService.Update(id, accountIn);

            return NoContent();
        }
    }

}
