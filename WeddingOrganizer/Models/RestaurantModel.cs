using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RestaurantModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LogoUrl { get; set; }
        public string Details { get; set; }
        public string MoreDetails { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Directions { get; set; }
    }
}
