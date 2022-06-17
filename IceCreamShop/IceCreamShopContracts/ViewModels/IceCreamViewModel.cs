using System.Collections.Generic;
using IceCreamShopContracts.Attributes;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.ViewModels
{
    [DataContract]
    public class IceCreamViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IceCreamName { get; set; }

        [DataMember]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
