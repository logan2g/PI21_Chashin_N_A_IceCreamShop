﻿using System;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopListImplement.Models;
using System.Collections.Generic;

namespace IceCreamShopListImplement.Imlements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;

        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfos)
            {
                result.Add(CreateModel(messageInfo));
            }
            return result;
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfos)
            {
                if ((model.ClientId.HasValue && messageInfo.ClientId == model.ClientId) ||
                (!model.ClientId.HasValue && messageInfo.DateDelivery.Date == model.DateDelivery.Date))
                {
                    result.Add(CreateModel(messageInfo));
                }
            }
            return result;
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var messageInfo in source.MessageInfos)
            {
                if (messageInfo.MessageId == model.MessageId)
                {
                    return CreateModel(messageInfo);
                }
            }
            return null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo tempMessageInfo = new MessageInfo { MessageId = "1" };
            foreach (var messageInfo in source.MessageInfos)
            {
                if (Convert.ToInt32(messageInfo.MessageId) >=
                    Convert.ToInt32(tempMessageInfo.MessageId))
                {
                    tempMessageInfo.MessageId = messageInfo.MessageId + 1;
                }
            }
            source.MessageInfos.Add(CreateModel(model, tempMessageInfo));
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo tempMessageInfo = null;
            foreach (var messageInfo in source.MessageInfos)
            {
                if (messageInfo.MessageId == model.MessageId)
                {
                    tempMessageInfo = messageInfo;
                }
            }
            if (tempMessageInfo == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempMessageInfo);
        }

        public void Delete(MessageInfoBindingModel model)
        {
            for (int i = 0; i < source.MessageInfos.Count; ++i)
            {
                if (source.MessageInfos[i].MessageId == model.MessageId)
                {
                    source.MessageInfos.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo messageInfo)
        {
            messageInfo.Subject = model.Subject;
            messageInfo.ClientId = model.ClientId;
            messageInfo.Body = model.Body;
            messageInfo.DateDelivery = model.DateDelivery;
            return messageInfo;
        }

        private MessageInfoViewModel CreateModel(MessageInfo messageInfo)
        {
            return new MessageInfoViewModel
            {
                MessageId = messageInfo.MessageId,
                SenderName = messageInfo.SenderName,
                Subject = messageInfo.Subject,
                Body = messageInfo.Body,
                DateDelivery = messageInfo.DateDelivery
            };
        }
    }
}