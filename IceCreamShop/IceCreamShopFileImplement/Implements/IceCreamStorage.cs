using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopFileImplement.Implements
{
    public class IceCreamStorage : IIceCreamStorage
    {
        private readonly FileDataSingleton source;

        public IceCreamStorage()
        {
            source = FileDataSingleton.GetInstance();
        }

        public List<IceCreamViewModel> GetFullList()
        {
            return source.IceCreams
                .Select(CreateModel)
                .ToList();
        }

        public List<IceCreamViewModel> GetFilteredList(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.IceCreams
                .Where(rec => rec.IceCreamName.Contains(model.IceCreamName))
                .Select(CreateModel)
                .ToList();
        }

        public IceCreamViewModel GetElement(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var product = source.IceCreams
                    .FirstOrDefault(rec => rec.IceCreamName == model.IceCreamName || rec.Id == model.Id);

            return product != null ? CreateModel(product) : null;
        }

        public void Insert(IceCreamBindingModel model)
        {
            int maxId = source.IceCreams.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new IceCream
            {
                Id = maxId + 1,
                IceCreamComponents = new Dictionary<int, int>()
            };
            source.IceCreams.Add(CreateModel(model, element));
        }

        public void Update(IceCreamBindingModel model)
        {
            var element = source.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }

        public void Delete(IceCreamBindingModel model)
        {
            IceCream element = source.IceCreams.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.IceCreams.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static IceCream CreateModel(IceCreamBindingModel model, IceCream product)
        {
            product.IceCreamName = model.IceCreamName;
            product.Price = model.Price;

            // удаляем убранные 
            foreach (var key in product.IceCreamComponents.Keys.ToList())
            {
                if (!model.IceCreamComponents.ContainsKey(key))
                {
                    product.IceCreamComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые 
            foreach (var component in model.IceCreamComponents)
            {
                if (product.IceCreamComponents.ContainsKey(component.Key))
                {
                    product.IceCreamComponents[component.Key] = model.IceCreamComponents[component.Key].Item2;
                }
                else
                {
                    product.IceCreamComponents.Add(component.Key, model.IceCreamComponents[component.Key].Item2);
                }
            }

            return product;
        }

        private IceCreamViewModel CreateModel(IceCream product)
        {
            return new IceCreamViewModel
            {
                Id = product.Id,
                IceCreamName = product.IceCreamName,
                Price = product.Price,
                IceCreamComponents = product.IceCreamComponents
                            .ToDictionary(recPC => recPC.Key, recPC => (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
            };
        }

    }
}
