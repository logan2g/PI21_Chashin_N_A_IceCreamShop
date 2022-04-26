using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.ViewModels
{
    [DataContract]
    public class IceCreamViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название изделия")]
        public string IceCreamName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
