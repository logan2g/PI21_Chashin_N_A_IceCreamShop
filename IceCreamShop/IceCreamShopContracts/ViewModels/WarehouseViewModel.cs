using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.ViewModels
{
    public class WarehouseViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string WarehouseName { get; set; }

        [DataMember]
        [DisplayName("ФИО ответственного")]
        public string ResponsiblePerson { get; set; }

        [DataMember]
        [DisplayName("Дата создания склада")]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
