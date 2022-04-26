using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.BindingModels
{
    [DataContract]
    public class IceCreamBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string IceCreamName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> IceCreamComponents { get; set; }
    }
}
