using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopContracts.BusinessLogicsContracts
{
    public interface IIceCreamLogic
    {
        List<IceCreamViewModel> Read(IceCreamBindingModel model);

        void CreateOrUpdate(IceCreamBindingModel model);

        void Delete(IceCreamBindingModel model);
    }
}
