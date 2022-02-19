using System.Collections.Generic;
using System.ComponentModel;

namespace IceCreamShopContracts.ViewModels
{
    public class IceCreamViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название изделия")]
        public string IceCreamName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
