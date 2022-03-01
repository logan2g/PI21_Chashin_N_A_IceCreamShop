using System.Collections.Generic;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;

namespace IceCreamShopContracts.BusinessLogicsContracts
{
    public interface IWarehouseLogic
    {
        List<WarehouseViewModel> Read(WarehouseBindingModel model);

        void CreateOrUpdate(WarehouseBindingModel model);

        void Delete(WarehouseBindingModel warehouse);

        void AddComponents(WarehouseBindingModel model, int componentId, int count);
    }
}
