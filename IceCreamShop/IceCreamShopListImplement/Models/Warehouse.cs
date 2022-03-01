using System;
using System.Collections.Generic;

namespace IceCreamShopListImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public string ResposiblePerson { get; set; }

        public DateTime CreateDate { get; set; }

        public Dictionary<int, int> WarehouseComponents { get; set; }
    }
}
