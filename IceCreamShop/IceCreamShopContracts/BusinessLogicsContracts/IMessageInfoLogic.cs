using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopContracts.BusinessLogicsContracts
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);

        void CreateOrUpdate(MessageInfoBindingModel model);
    }
}
