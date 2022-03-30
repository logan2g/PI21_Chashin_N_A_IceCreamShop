using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<IceCreamViewModel> IceCreams { get; set; }

        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}
