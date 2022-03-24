using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;

        private readonly IIceCreamLogic _product;

        public MainController(IOrderLogic order, IIceCreamLogic product)
        {
            _order = order;
            _product = product;
        }

        [HttpGet]
        public List<IceCreamViewModel> GetIceCreamList() => _product.Read(null)?.ToList();

        [HttpGet]
        public IceCreamViewModel GetIceCream(int iceCreamId) => _product.Read(new IceCreamBindingModel { Id = iceCreamId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
    }

}
