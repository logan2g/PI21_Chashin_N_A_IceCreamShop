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
    public class IceCreamStorage : IIceCreamStorage
    {
        public List<IceCreamViewModel> GetFullList()
        {
            using var context = new IceCreamShopDatabase();
            return context.IceCreams
              .Include(rec => rec.IceCreamComponents)
              .ThenInclude(rec => rec.Component)
              .ToList()
              .Select(CreateModel)
              .ToList();
        }

        public List<IceCreamViewModel> GetFilteredList(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new IceCreamShopDatabase();
            return context.IceCreams
              .Include(rec => rec.IceCreamComponents)
              .ThenInclude(rec => rec.Component)
              .Where(rec => rec.IceCreamName.Contains(model.IceCreamName))
              .ToList()
              .Select(CreateModel)
              .ToList();
        }

        public IceCreamViewModel GetElement(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new IceCreamShopDatabase();
            var product = context.IceCreams
              .Include(rec => rec.IceCreamComponents)
              .ThenInclude(rec => rec.Component)
              .FirstOrDefault(rec => rec.IceCreamName == model.IceCreamName || rec.Id == model.Id);

            return product != null ? CreateModel(product) : null;
        }

        public void Insert(IceCreamBindingModel model)
        {
            using var context = new IceCreamShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.IceCreams.Add(CreateModel(model, new IceCream(), context));
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(IceCreamBindingModel model)
        {
            using var context = new IceCreamShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(IceCreamBindingModel model)
        {
            using var context = new IceCreamShopDatabase();
            IceCream element = context.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.IceCreams.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static IceCream CreateModel(IceCreamBindingModel model, IceCream product, IceCreamShopDatabase context)
        {
            product.IceCreamName = model.IceCreamName;
            product.Price = model.Price;

            if (model.Id.HasValue)
            {
                var productComponents = context.IceCreamComponents.Where(rec => rec.IceCreamId == model.Id.Value).ToList();
                // удалили те, которых нет в модели 
                context.IceCreamComponents.RemoveRange(productComponents.Where(rec => !model.IceCreamComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей 
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Count = model.IceCreamComponents[updateComponent.ComponentId].Item2;
                    model.IceCreamComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые 
            foreach (var pc in model.IceCreamComponents)
            {
                context.IceCreamComponents.Add(new IceCreamComponent
                {
                    IceCreamId = product.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }

            return product;
        }

        private static IceCreamViewModel CreateModel(IceCream product)
        {
            return new IceCreamViewModel
            {
                Id = product.Id,
                IceCreamName = product.IceCreamName,
                Price = product.Price,
                IceCreamComponents = product.IceCreamComponents
                    .ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }

}
