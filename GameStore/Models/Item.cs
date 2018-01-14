using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PicUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}