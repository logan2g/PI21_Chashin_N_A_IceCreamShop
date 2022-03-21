using System;

namespace IceCreamShopContracts.ViewModels
{
    public class ReportOrderViewModel
    {
        public DateTime DateCreate { get; set; }

        public string IceCreamName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }
    }
}
