using System;
using System.Collections.Generic;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;

namespace IceCreamShopBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;

        private readonly IComponentStorage _componentStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
        {
            _warehouseStorage = warehouseStorage;
            _componentStorage = componentStorage;
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                WarehouseName = model.WarehouseName,
                WarehouseComponents = model.WarehouseComponents
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel warehouse)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = warehouse.Id
            });
            _warehouseStorage.Delete(warehouse);
        }

        public void AddComponents(WarehouseTopUpBindingModel model)
        {
            var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.WarehouseId });
            if (warehouse.WarehouseComponents.ContainsKey(model.ComponentId))
            {
                warehouse.WarehouseComponents[model.ComponentId] =
                    (warehouse.WarehouseComponents[model.ComponentId].Item1, warehouse.WarehouseComponents[model.ComponentId].Item2 + model.Count);
            }
            else
            {
                var component = _componentStorage.GetElement(new ComponentBindingModel
                {
                    Id = model.ComponentId
                });
                if (component == null)
                {
                    throw new Exception("Компонент не найден");
                }
                warehouse.WarehouseComponents.Add(model.ComponentId, (component.ComponentName, model.Count));
            }
            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsiblePerson = warehouse.ResponsiblePerson,
                CreateDate = warehouse.CreateDate,
                WarehouseComponents = warehouse.WarehouseComponents
            });
        }
    }
}
