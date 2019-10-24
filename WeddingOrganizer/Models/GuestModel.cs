using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GuestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsInvited { get; set; }
        public bool Confirmed { get; set; }
        public bool HasPlusOne { get; set; }
        public UserModel User{ get; set; }
    }
}
