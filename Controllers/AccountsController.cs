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

        [HttpGet]
        public ActionResult<List<Account>> Get() =>
            _accountService.Get();

        [HttpPost("login")]
        public JsonResult Login([FromBody]LoginCreds loginCreds)
        {
            if (_accountService.Find(loginCreds.Username, loginCreds.Password) != null)
                return new JsonResult(new { status = 302 });
            else 
                return new JsonResult(new { status = 404 });
        }

        [HttpPost("register")]
        public JsonResult Login([FromBody]Account account)
        {
            if (_accountService.Create(account) != null)
                return new JsonResult(new { status = 302 });
            else
                return new JsonResult(new { status = 404 });
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
            return CreatedAtRoute("GetAccount", new { id = account.Id.ToString() }, account);
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

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var service = _accountService.Get(id);

            if (service == null)
            {
                return NotFound();
            }

            _accountService.Remove(service.Id);

            return NoContent();
        }
    }

}
