using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IceCreamShopContracts.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string WarehouseName { get; set; }

        [DisplayName("ФИО ответственного")]
        public string ResposiblePerson { get; set; }

        [DisplayName("Дата создания склада")]
        public DateTime CreateDate { get; set; }

        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
