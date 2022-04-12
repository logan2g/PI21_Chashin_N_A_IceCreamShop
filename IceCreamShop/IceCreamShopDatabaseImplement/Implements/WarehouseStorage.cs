using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public void Delete(WarehouseBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Warehouses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new IceCreamShopDatabase())
            {
                var warehouse = context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
                return warehouse != null ?
                new WarehouseViewModel
                {
                    Id = warehouse.Id,
                    WarehouseName = warehouse.WarehouseName,
                    ResponsiblePerson = warehouse.ResponsiblePerson,
                    CreateDate = warehouse.CreateDate,
                    WarehouseComponents = warehouse.WarehouseComponents
                    .ToDictionary(recSC => recSC.ComponentId, recSC => (recSC.Component?.ComponentName, recSC.Count))
                } :
                null;
            }
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new IceCreamShopDatabase())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .ToList()
                .Select(rec => new WarehouseViewModel
                {
                    Id = rec.Id,
                    WarehouseName = rec.WarehouseName,
                    ResponsiblePerson = rec.ResponsiblePerson,
                    CreateDate = rec.CreateDate,
                    WarehouseComponents = rec.WarehouseComponents
                    .ToDictionary(recSC => recSC.ComponentId, recSC => (recSC.Component?.ComponentName, recSC.Count))
                })
                .ToList();
            }
        }

        public List<WarehouseViewModel> GetFullList()
        {
            using (var context = new IceCreamShopDatabase())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new WarehouseViewModel
                {
                    Id = rec.Id,
                    WarehouseName = rec.WarehouseName,
                    ResponsiblePerson = rec.ResponsiblePerson,
                    CreateDate = rec.CreateDate,
                    WarehouseComponents = rec.WarehouseComponents
                    .ToDictionary(recSC => recSC.ComponentId, recSC => (recSC.Component?.ComponentName, recSC.Count))
                })
                .ToList();
            }
        }

        public void Insert(WarehouseBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Warehouse warehouse = CreateModel(model, new Warehouse(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(WarehouseBindingModel model)
        {
            using (var context = new IceCreamShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Склад не найден");
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
            }
        }

        public void TakeFromWarehouse(IceCreamViewModel model, int countInOrder)
        {
            using (var context = new IceCreamShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    foreach (var snackComponent in model.IceCreamComponents)
                    {
                        int componentCount = snackComponent.Value.Item2 * countInOrder;

                        List<WarehouseComponent> certainIngredients = context.WarehouseComponents
                            .Where(kitchen => kitchen.ComponentId == snackComponent.Key)
                            .ToList();

                        foreach (var component in certainIngredients)
                        {
                            int componentCountInWarehouse = component.Count;

                            if (componentCountInWarehouse <= componentCount)
                            {
                                componentCount -= componentCountInWarehouse;
                                context.Warehouses.FirstOrDefault(rec => rec.Id == component.WarehouseId).WarehouseComponents.Remove(component);
                            }
                            else
                            {
                                component.Count -= componentCount;
                                componentCount = 0;
                            }

                            if (componentCount == 0)
                            {
                                break;
                            }
                        }

                        if (componentCount > 0)
                        {
                            transaction.Rollback();

                            throw new Exception("Не хватает компонентов для продукта!");
                        }
                    }

                    context.SaveChanges();

                    transaction.Commit();
                }
            }
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, IceCreamShopDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsiblePerson = model.ResponsiblePerson;
            if (warehouse.Id == 0)
            {
                warehouse.CreateDate = DateTime.Now;
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
            }
            if (model.Id.HasValue && model.WarehouseComponents != null)
            {
                var warehouseComponents = context.WarehouseComponents.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.WarehouseComponents.RemoveRange(warehouseComponents.Where(rec => !model.WarehouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in warehouseComponents)
                {
                    updateComponent.Count = model.WarehouseComponents[updateComponent.ComponentId].Item2;
                    model.WarehouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
                // добавили новые
                foreach (var sc in model.WarehouseComponents)
                {
                    context.WarehouseComponents.Add(new WarehouseComponent
                    {
                        WarehouseId = warehouse.Id,
                        ComponentId = sc.Key,
                        Count = sc.Value.Item2
                    });
                    context.SaveChanges();
                }
            }
            
            return warehouse;
        }


    }
}
