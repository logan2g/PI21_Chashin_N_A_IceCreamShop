using System;
using System.Collections.Generic;

namespace IceCreamShopFileImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public string ResponsiblePerson { get; set; }

        public DateTime CreateDate { get; set; }

        public Dictionary<int, int> WarehouseComponents { get; set; }
    }
}
