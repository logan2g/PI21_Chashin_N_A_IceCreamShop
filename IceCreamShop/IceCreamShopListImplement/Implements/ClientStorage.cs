using IceCreamShopListImplement.Models;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;
using System;

namespace IceCreamShopListImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly DataListSingleton source;

        public ClientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            List<ClientViewModel> result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                result.Add(CreateModel(client));
            }
            return result;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<ClientViewModel>();
            foreach(var client in source.Clients)
            {
                if(client.ClientFIO == model.ClientFIO && client.Password == model.Password)
                {
                    result.Add(CreateModel(client));
                }
            }
            return result;
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var client in source.Clients)
            {
                if (client.ClientFIO == model.ClientFIO || client.Password == model.Password)
                {
                    return CreateModel(client);
                }
            }

            return null;
        }

        public void Insert(ClientBindingModel model)
        {
            Client tmpClient = new Client { Id = 1 };
            foreach (var client in source.Clients)
            {
                if(client.Id >= tmpClient.Id)
                {
                    tmpClient.Id = client.Id + 1;
                }
            }

            source.Clients.Add(CreateModel(model, tmpClient));
        }

        public void Update(ClientBindingModel model)
        {
            Client tmpClient = null;
            foreach (var client in source.Clients)
            {
                if (client.Id == model.Id)
                {
                    tmpClient = client;
                    break;
                }
            }

            if (tmpClient == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tmpClient);
        }

        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
