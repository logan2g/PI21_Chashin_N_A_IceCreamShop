using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportIceCreamComponentViewModel> GetIceCreams();

        List<ReportOrderViewModel> GetOrders(ReportBindingModel model);

        void SaveIceCreamsToWordFile(ReportBindingModel model);

        void SaveIceCreamComponentToExcelFile(ReportBindingModel model);

        void SaveOrdersToPdfFile(ReportBindingModel model);

    }
}
