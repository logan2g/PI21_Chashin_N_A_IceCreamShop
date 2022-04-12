using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportIceCreamComponentViewModel> IceCreams { get; set; }

        public List<ReportWarehouseComponentViewModel> Warehouses { get; set; }
    }
}
