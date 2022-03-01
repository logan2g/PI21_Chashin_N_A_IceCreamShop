using System;
using System.Collections.Generic;

namespace IceCreamShopContracts.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }

        public string WarehouseName;

        public string ResposiblePerson { get; set; }

        public DateTime CreateDate { get; set; }

        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
