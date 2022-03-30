using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportIceCreamComponentViewModel> GetIceCreams();

        List<ReportOrderViewModel> GetOrders(ReportBindingModel model);

        List<ReportOrderViewModel> GetOrdersFull();

        List<ReportWarehouseComponentViewModel> GetWarehouses();

        void SaveIceCreamsToWordFile(ReportBindingModel model);

        void SaveWarehousesToWordFile(ReportBindingModel model);

        void SaveIceCreamComponentToExcelFile(ReportBindingModel model);

        void SaveWarehousesComponentToExcelFile(ReportBindingModel model);

        void SaveOrdersByDateToPdfFile(ReportBindingModel model);

        void SaveOrdersFullToPdfFile(ReportBindingModel model);

    }
}
