using System;

namespace Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Age { get; set; }
        public DateTime WeddingDate { get; set; }
        public string PartnerName { get; set; }
        public byte PartnerAge { get; set; }
    }
}
