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
        //ROUTE TO LOGIN INTO SYSTEM
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
        //ROUTE TO ADD GOOD TO CART
        [HttpPost("cart/add")]
        public JsonResult AddToCart([FromBody]CartCredsAdd cardCreds)
        {
            return _accountService.AddToCart(cardCreds.Id, cardCreds.service);
        }
        //ROUTE TO DELETE GOOD FROM CART
        [HttpPost("cart/delete")]
        public JsonResult DeleteFromCart([FromBody]CartCredsDelete cardCreds)
        {
            return _accountService.DeleteFromCart(cardCreds.Id, cardCreds.service);
        }
        //ROUTE TO CLEAR CART
        [HttpPost("cart/drop")]
        public JsonResult DropCart([FromBody]DropCartCreds creds)
        {
            return _accountService.DropCart(creds.id);
        }
        //ROUTE TO REGISTER INTO SYSTEM
        [HttpPost("register")]
        public JsonResult Register([FromBody]Account account)
        {
            if(_accountService.Find(account.username) != null)
            {
                return new JsonResult(new { status = 400, error = "User already exists..." }); ;
            }
            return _accountService.Create(account);
        }
    }

}
