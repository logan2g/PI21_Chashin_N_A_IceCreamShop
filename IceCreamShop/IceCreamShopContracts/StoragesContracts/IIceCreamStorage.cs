using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopContracts.StoragesContracts
{
    public interface IIceCreamStorage
    {
        List<IceCreamViewModel> GetFullList();

        List<IceCreamViewModel> GetFilteredList(IceCreamBindingModel model);

        IceCreamViewModel GetElement(IceCreamBindingModel model);

        void Insert(IceCreamBindingModel model);

        void Update(IceCreamBindingModel model);

        void Delete(IceCreamBindingModel model);
    }
}
