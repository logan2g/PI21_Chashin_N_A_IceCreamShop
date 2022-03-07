﻿using System.Collections.Generic;

namespace IceCreamShopFileImplement.Models
{
    public class IceCream
    {
        public int Id { get; set; }

        public string IceCreamName { get; set; }

        public decimal Price { get; set; }

        public Dictionary<int, int> IceCreamComponents { get; set; }
    }
}
