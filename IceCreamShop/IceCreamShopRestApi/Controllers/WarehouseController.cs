using Microsoft.AspNetCore.Mvc;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;

namespace IceCreamShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController
    {
        private readonly IWarehouseLogic _warehouses;
        private readonly IComponentLogic _components;

        public WarehouseController(IWarehouseLogic warehouse, IComponentLogic component)
        {
            _warehouses = warehouse;
            _components = component;
        }

        [HttpGet]
        public List<WarehouseViewModel> GetWarehouses() => _warehouses.Read(null);

        [HttpPost]
        public void CreateWarehouse(WarehouseBindingModel model) => _warehouses.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateWarehouse(WarehouseBindingModel model) => _warehouses.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWarehouse(WarehouseBindingModel model) => _warehouses.Delete(model);

        [HttpPost]
        public void TopUp(WarehouseTopUpBindingModel model) => _warehouses.AddComponents(model);

        [HttpGet]
        public List<ComponentViewModel> GetComponents() => _components.Read(null);
    }
}
