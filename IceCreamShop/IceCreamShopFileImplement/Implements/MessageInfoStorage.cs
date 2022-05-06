using System;
using System.Collections.Generic;
using System.Linq;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopFileImplement.Models;

namespace IceCreamShopFileImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly FileDataSingleton source;

        public MessageInfoStorage()
        {
            source = FileDataSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            return source.MessageInfos
            .Select(CreateModel)
            .ToList();
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.MessageInfos
            .Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date))
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }

            source.MessageInfos.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                IsRead = false
            });
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element == null)
            {
                throw new Exception("Не найдено письмо с таким идентификатором");
            }

            if (model.IsRead.HasValue)
            {
                element.IsRead = model.IsRead.Value;
            }

            if (!string.IsNullOrEmpty(model.Reply))
            {
                element.ReplyText = model.Reply;
            }
        }

        private MessageInfoViewModel CreateModel(MessageInfo message)
        {
            return new MessageInfoViewModel
            {
                MessageId = message.MessageId,
                SenderName = message.SenderName,
                DateDelivery = message.DateDelivery,
                Subject = message.Subject,
                Body = message.Body,
                Reply = message.ReplyText
            };
        }
    }
}
