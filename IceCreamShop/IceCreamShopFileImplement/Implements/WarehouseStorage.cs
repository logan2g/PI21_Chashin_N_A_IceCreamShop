using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataSingleton source;

        public WarehouseStorage()
        {
            source = FileDataSingleton.GetInstance();
        }

        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses
            .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
            .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
            .Select(CreateModel)
            .ToList();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses
             .Select(CreateModel)
             .ToList();
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var element = new Warehouse { Id = maxId + 1, WarehouseComponents = new Dictionary<int, int>(), CreateDate = DateTime.Now };
            source.Warehouses.Add(CreateModel(model, element));
        }

        public bool TakeFromWarehouse(Dictionary<int, (string, int)> whcomponents, int componentCount)
        {
            foreach (var whComponent in whcomponents)
            {
                int count = source.Warehouses.Where(component => component.WarehouseComponents.ContainsKey(whComponent.Key)).Sum(material => material.WarehouseComponents[whComponent.Key]);

                if (count < whComponent.Value.Item2 * componentCount)
                {
                    return false;
                }
            }

            foreach (var whComponent in whcomponents)
            {
                int count = whComponent.Value.Item2 * componentCount;
                IEnumerable<Warehouse> components = source.Warehouses.Where(material => material.WarehouseComponents.ContainsKey(whComponent.Key));

                foreach (Warehouse kitchen in components)
                {
                    if (kitchen.WarehouseComponents[whComponent.Key] <= count)
                    {
                        count -= kitchen.WarehouseComponents[whComponent.Key];
                        kitchen.WarehouseComponents.Remove(whComponent.Key);
                    }
                    else
                    {
                        kitchen.WarehouseComponents[whComponent.Key] -= count;
                        count = 0;
                    }

                    if (count == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsiblePerson = warehouse.ResponsiblePerson,
                CreateDate = warehouse.CreateDate,
                WarehouseComponents = warehouse.WarehouseComponents.ToDictionary(recSC => recSC.Key, recSC =>
                (source.Components.FirstOrDefault(recC => recC.Id == recSC.Key)?.ComponentName, recSC.Value))
            };
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsiblePerson = model.ResponsiblePerson;
            // удаляем убранные
            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] = model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key, model.WarehouseComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }

    }
}
