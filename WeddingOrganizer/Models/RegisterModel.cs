using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public byte Age { get; set; }
        public DateTime WeddingDate { get; set; }
        public string PartnerName { get; set; }
        public byte PartnerAge { get; set; }
    }
}
