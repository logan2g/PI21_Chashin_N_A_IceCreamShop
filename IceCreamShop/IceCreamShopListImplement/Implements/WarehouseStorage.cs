using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopListImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;

        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id || warehouse.WarehouseName == model.WarehouseName)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();
            foreach (var component in source.Warehouses)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<WarehouseViewModel> GetFullList()
        {
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                result.Add(CreateModel(warehouse));
            }
            return result;
        }

        public void Insert(WarehouseBindingModel model)
        {
            Warehouse tempWarhouse = new Warehouse
            {
                Id = 1,
                WarehouseComponents = new Dictionary<int, int>(),
                CreateDate = DateTime.Now
            };
            foreach (var warhouse in source.Warehouses)
            {
                if (warhouse.Id >= tempWarhouse.Id)
                {
                    tempWarhouse.Id = warhouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWarhouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarhouse = null;
            foreach (var tempWh in source.Warehouses)
            {
                if (tempWh.Id == model.Id)
                {
                    tempWarhouse = tempWh;
                }
            }
            if (tempWarhouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWarhouse);
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResposiblePerson = model.ResposiblePerson;
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

        private WarehouseViewModel CreateModel(Warehouse wh)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> warehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var whComponents in wh.WarehouseComponents)
            {
                string wComponentsName = string.Empty;
                foreach (var warehouse in source.Warehouses)
                {
                    if (whComponents.Key == warehouse.Id)
                    {
                        wComponentsName = warehouse.WarehouseName;
                        break;
                    }
                }
                warehouseComponents.Add(whComponents.Key, (wComponentsName, whComponents.Value));
            }
            return new WarehouseViewModel
            {
                Id = wh.Id,
                WarehouseName = wh.WarehouseName,
                ResposiblePerson = wh.ResposiblePerson,
                CreateDate = wh.CreateDate,
                WarehouseComponents = warehouseComponents
            };
        }
    }
}
