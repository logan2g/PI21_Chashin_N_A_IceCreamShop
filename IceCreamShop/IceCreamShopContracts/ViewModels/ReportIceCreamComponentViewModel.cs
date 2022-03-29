using System;
using System.Collections.Generic;

namespace IceCreamShopContracts.ViewModels
{
    public class ReportIceCreamComponentViewModel
    {
        public string IceCreamName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Components { get; set; }
    }
}
