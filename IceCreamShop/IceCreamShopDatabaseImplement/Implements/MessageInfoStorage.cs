using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopContracts.BindingModels;
using IceCreamShopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopDatabaseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly int stringsOnPage = 7;

        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new IceCreamShopDatabase();
            return context.MessageInfos
            .Select(CreateModel)
            .ToList();
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new IceCreamShopDatabase();
            var messageInfoes = context.MessageInfos
            .Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date) ||
            (!model.ClientId.HasValue && model.PageNumber.HasValue) ||
            (model.ClientId.HasValue && rec.ClientId == model.ClientId && model.PageNumber.HasValue));

            if (model.PageNumber.HasValue)
            {
                messageInfoes = messageInfoes.Skip(stringsOnPage * (model.PageNumber.Value - 1))
                    .Take(stringsOnPage);
            }

            return messageInfoes.Select(CreateModel).ToList();
        }

        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new IceCreamShopDatabase();
            MessageInfo element = context.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);

            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }

            var client = context.Clients.FirstOrDefault(rec => rec.Email.Equals(model.FromMailAddress));

            context.MessageInfos.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = client?.Id,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                IsRead = false
            });
            context.SaveChanges();
        }

        public void Update(MessageInfoBindingModel model)
        {
            using var context = new IceCreamShopDatabase();
            MessageInfo element = context.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);

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

            context.SaveChanges();
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
                Reply = message.ReplyText,
                IsRead = message.IsRead
            };
        }
    }
}
