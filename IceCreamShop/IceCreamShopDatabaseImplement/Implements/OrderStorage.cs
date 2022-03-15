using IceCreamShopDatabaseImplement.Models;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
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
            if (model.DateFrom != null && model.DateTo != null)
            {
                using (var context = new IceCreamShopDatabase())
                {
                    return context.Orders
                        .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                        .Select(rec => new OrderViewModel
                        {
                            Id = rec.Id,
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
            using (var context = new IceCreamShopDatabase())
            {
                return context.Orders
                .Where(rec => rec.IceCreamId == model.IceCreamId)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
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
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new IceCreamShopDatabase())
            {
                var order = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    IceCreamId = order.IceCreamId,
                    IceCreamName = order?.IceCreamName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                Order order = new Order
                {
                    IceCreamId = model.IceCreamId,
                    IceCreamName = context.IceCreams.FirstOrDefault(pr => pr.Id == model.IceCreamId).IceCreamName,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
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
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
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
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new IceCreamShopDatabase())
            {
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
            }
            return order;
        }
    }
}
