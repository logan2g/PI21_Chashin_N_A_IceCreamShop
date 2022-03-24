using IceCreamShopDatabaseImplement.Models;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new IceCreamShopDatabase())
            {
                return context.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = context.Clients.FirstOrDefault(cl => cl.Id == rec.ClientId).ClientFIO,
                    IceCreamId = rec.IceCreamId,
                    IceCreamName = rec.IceCreamName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new IceCreamShopDatabase())
            {
                return context.Orders
                .Include(rec => rec.IceCream)
                .Include(rec => rec.Client)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
            (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
            (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    IceCreamId = rec.IceCreamId,
                    IceCreamName = rec.IceCreamName,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new IceCreamShopDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    IceCreamId = order.IceCreamId,
                    ClientId = order.ClientId,
                    IceCreamName = order?.IceCreamName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } : null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                if (!model.ClientId.HasValue)
                {
                    throw new Exception("Клиент не указан");
                }
                Order order = new Order
                {
                    IceCreamId = model.IceCreamId,
                    ClientId = (int)model.ClientId,
                    IceCreamName = context.IceCreams.FirstOrDefault(pr => pr.Id == model.IceCreamId).IceCreamName,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order, context);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.IceCreamId = model.IceCreamId;
                element.ClientId = (int)model.ClientId;
                element.IceCreamName = context.IceCreams.FirstOrDefault(pr => pr.Id == model.IceCreamId).IceCreamName;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order, IceCreamShopDatabase context)
        {
            if (model == null)
            {
                return null;
            }

            //using (var context = new IceCreamShopDatabase())
            //{
                IceCream element = context.IceCreams.FirstOrDefault(rec => rec.Id == model.IceCreamId);
                if (element != null)
                {
                    if (element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                    element.Orders.Add(order);
                    context.IceCreams.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
                Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
                if (client != null)
                {
                    if (client.Order == null)
                    {
                        client.Order = new List<Order>();
                    }
                    client.Order.Add(order);
                    context.Clients.Update(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            //}
            return order;
        }
    }
}
