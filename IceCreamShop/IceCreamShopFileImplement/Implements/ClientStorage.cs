﻿using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopFileImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly FileDataSingleton source;

        public ClientStorage()
        {
            source = FileDataSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            return source.Clients
                    .Select(CreateModel)
                    .ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Clients
                    .Where(rec => rec.ClientFIO.Contains(model.ClientFIO) && rec.Password.Contains(model.Password))
                    .Select(CreateModel)
                    .ToList();
        }


        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
            var element = new Client { Id = maxId + 1 };
            source.Clients.Add(CreateModel(model, element));
        }

        public void Update(ClientBindingModel model)
        {
            var element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Password = model.Password;
            client.Email = model.Email;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                ClientFIO = client.ClientFIO,
                Password = client.Password,
                Email = client.Email
            };
        }
    }
}
