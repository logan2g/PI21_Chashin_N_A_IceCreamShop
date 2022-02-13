using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopListImplement.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IceCreamShopListImplement.Implements
{
    public class IceCreamStorage : IIceCreamStorage
    {
        private readonly DataListSingleton source;

        public IceCreamStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<IceCreamViewModel> GetFullList()
        {
            var result = new List<IceCreamViewModel>();
            foreach (var component in source.IceCreams)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<IceCreamViewModel> GetFilteredList(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<IceCreamViewModel>();
            foreach (var icecream in source.IceCreams)
            {
                if (icecream.IceCreamName.Contains(model.IceCreamName))
                {
                    result.Add(CreateModel(icecream));
                }
            }
            return result;
        }

        public IceCreamViewModel GetElement(IceCreamBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var icecream in source.IceCreams)
            {
                if (icecream.Id == model.Id || icecream.IceCreamName == model.IceCreamName)
                {
                    return CreateModel(icecream);
                }
            }

            return null;
        }

        public void Insert(IceCreamBindingModel model)
        {
            var tempIceCream = new IceCream
            {
                Id = 1,
                IceCreamComponents = new Dictionary<int, int>()
            };
            foreach (var icecream in source.IceCreams)
            {
                if (icecream.Id >= tempIceCream.Id)
                {
                    tempIceCream.Id = icecream.Id + 1;
                }
            }

            source.IceCreams.Add(CreateModel(model, tempIceCream));
        }

        public void Update(IceCreamBindingModel model)
        {
            IceCream tempIceCream = null;
            foreach (var icecream in source.IceCreams)
            {
                if (icecream.Id == model.Id)
                {
                    tempIceCream = icecream;
                }
            }

            if (tempIceCream == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tempIceCream);
        }

        public void Delete(IceCreamBindingModel model)
        {
            for (int i = 0; i < source.IceCreams.Count; ++i)
            {
                if (source.IceCreams[i].Id == model.Id)
                {
                    source.IceCreams.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static IceCream CreateModel(IceCreamBindingModel model, IceCream icecream)
        {
            icecream.IceCreamName = model.IceCreamName;
            icecream.Price = model.Price;

            // удаляем убранные 
            foreach (var key in icecream.IceCreamComponents.Keys.ToList())
            {
                if (!model.IceCreamComponents.ContainsKey(key))
                {
                    icecream.IceCreamComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые 
            foreach (var component in model.IceCreamComponents)
            {
                if (icecream.IceCreamComponents.ContainsKey(component.Key))
                {
                    icecream.IceCreamComponents[component.Key] = model.IceCreamComponents[component.Key].Item2;
                }
                else
                {
                    icecream.IceCreamComponents.Add(component.Key, model.IceCreamComponents[component.Key].Item2);
                }
            }

            return icecream;
        }

        private IceCreamViewModel CreateModel(IceCream icecream)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количеством
            var icecreamComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in icecream.IceCreamComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                icecreamComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new IceCreamViewModel
            {
                Id = icecream.Id,
                IceCreamName = icecream.IceCreamName,
                Price = icecream.Price,
                IceCreamComponents = icecreamComponents
            };
        }

    }
}
