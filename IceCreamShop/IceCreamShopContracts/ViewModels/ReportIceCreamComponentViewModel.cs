using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.ViewModels
{
    [DataContract]
    public class ReportIceCreamComponentViewModel
    {
        [DataMember]
        public string IceCreamName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public List<Tuple<string, int>> Components { get; set; }
    }
}
