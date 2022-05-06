using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.BusinessLogics
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly IMessageInfoStorage _messageInfoStorage;

        public MessageInfoLogic(IMessageInfoStorage messageInfoStorage)
        {
            _messageInfoStorage = messageInfoStorage;
        }

        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return _messageInfoStorage.GetFullList();
            }

            return _messageInfoStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MessageInfoBindingModel model)
        {
            if (model.IsRead.HasValue || !string.IsNullOrEmpty(model.Reply))
            {
                _messageInfoStorage.Update(model);
            }
            else
            {
                _messageInfoStorage.Insert(model);
            }
        }
    }
}
