using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LogoUrl { get; set; }
        public string Details { get; set; }
        public string MoreDetails { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Directions { get; set; }
        public virtual IQueryable<User> Users { get; set; }
    }
}
