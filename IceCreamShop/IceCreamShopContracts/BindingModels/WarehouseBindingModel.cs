using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.BindingModels
{
    public class WarehouseBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string WarehouseName { get; set; }

        [DataMember]
        public string ResponsiblePerson { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
