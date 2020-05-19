using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework_kpiyap.Models
{
    public class RegisterCreds
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public List<Object> Cart { get; set; }
    }
}
