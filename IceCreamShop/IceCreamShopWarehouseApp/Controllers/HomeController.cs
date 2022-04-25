using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.ViewModels;
using IceCreamShopWarehouseApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace IceCreamShopWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (!Program.IsAuthorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouses"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                bool check = password == _configuration["Password"];

                if (!check)
                {
                    throw new Exception("Попробуйте другой пароль");
                }

                Program.IsAuthorized = check;
                Response.Redirect("Index");
                return;
            }

            throw new Exception("Введите пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string name, string FIO)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(FIO))
            {
                return;
            }
            APIClient.PostRequest("api/warehouse/createwarehouse", new WarehouseBindingModel
            {
                WarehouseName = name,
                ResponsiblePerson = FIO,
                WarehouseComponents = new Dictionary<int, (string, int)>()
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("Склад не найден");
            }

            var storeHouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouses").FirstOrDefault(rec => rec.Id == id);
            if (storeHouse == null)
            {
                throw new Exception("Склад не найден");
            }

            return View(storeHouse);
        }

        [HttpPost]
        public IActionResult Edit(int id, string warehouseName, string FIO)
        {
            var storeHouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouses").FirstOrDefault(rec => rec.Id == id);

            APIClient.PostRequest("api/warehouse/updatewarehouse", new WarehouseBindingModel
            {
                Id = id,
                WarehouseName = warehouseName,
                ResponsiblePerson = FIO,
                WarehouseComponents = storeHouse.WarehouseComponents
            });
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Склад не найден");
            }

            var wareHouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouses").FirstOrDefault(rec => rec.Id == id);
            if (wareHouse == null)
            {
                throw new Exception("Склад не найден");
            }

            return View(wareHouse);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            APIClient.PostRequest("api/warehouse/deletewarehouse", new WarehouseBindingModel { Id = id });
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult TopUp()
        {
            if (!Program.IsAuthorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouses");
            ViewBag.Components = APIClient.GetRequest<List<ComponentViewModel>>($"api/warehouse/getcomponents");

            return View();
        }

        [HttpPost]
        public IActionResult TopUp(int wareHouseId, int componentId, int count)
        {
            if (wareHouseId == 0 || componentId == 0 || count <= 0)
            {
                throw new Exception("Проверьте данные");
            }

            var storeHouse = APIClient.GetRequest<List<WarehouseViewModel>>(
                 $"api/warehouse/getwarehouses").FirstOrDefault(rec => rec.Id == wareHouseId);

            if (storeHouse == null)
            {
                throw new Exception("Склад не найден");
            }

            var component = APIClient.GetRequest<List<WarehouseViewModel>>(
                $"api/warehouse/getcomponents").FirstOrDefault(rec => rec.Id == componentId);

            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }

            APIClient.PostRequest("api/warehouse/topup", new WarehouseTopUpBindingModel
            {
                WarehouseId = wareHouseId,
                ComponentId = componentId,
                Count = count
            });
            return Redirect("~/Home/Index");
        }
    }
}
