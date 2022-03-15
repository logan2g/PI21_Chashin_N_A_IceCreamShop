using System;
using System.Collections.Generic;
using IceCreamShopContracts.ViewModels;

namespace IceCreamShopBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportOrderViewModel> Orders { get; set; }
    }
}
