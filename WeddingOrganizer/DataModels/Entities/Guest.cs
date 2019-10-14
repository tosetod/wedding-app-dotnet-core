using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; }
        public bool IsInvited { get; set; }
        public bool Confirmed { get; set; }
        public bool HasPlusOne { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
