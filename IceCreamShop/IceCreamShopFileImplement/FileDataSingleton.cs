using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using IceCreamShopFileImplement.Models;
using IceCreamShopContracts.Enums;

namespace IceCreamShopFileImplement
{
    public class FileDataSingleton
    {
        private static FileDataSingleton instance;

        private readonly string ComponentFileName = "Component.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string IceCreamFileName = "IceCream.xml";

        private readonly string ClientFileName = "Client.xml";

        private readonly string ImplementerFileName = "Implemeter.xml";

        private readonly string MailsFileName = "Mails.xml";

        private readonly string WarehouseFileName = "Warehouse.xml";

        public List<Component> Components { get; set; }

        public List<Order> Orders { get; set; }

        public List<IceCream> IceCreams { get; set; }

        public List<Client> Clients { get; set; }

        public List<MessageInfo> MessageInfos { get; set; }

        public List<Implementer> Implementers { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        private FileDataSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            IceCreams = LoadIceCreams();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessageInfos = LoadMessageInfos();
            Warehouses = LoadWarehouses();
        }

        ~FileDataSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveIceCreams();
            SaveClients();
            SaveImplementers();
            SaveMessageInfos();
            SaveWarehouses();
        }

        public static FileDataSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataSingleton();
            }
            return instance;
        }

        private List<MessageInfo> LoadMessageInfos()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MailsFileName))
            {
                XDocument xDocument = XDocument.Load(MailsFileName);
                var xElements = xDocument.Root.Elements("MessageInfo").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("Id").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        SenderName = elem.Element("ClientId").Value,
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value)
                    });
                }
            }
            return list;
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IceCreamId = Convert.ToInt32(elem.Element("IceCreamId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToInt32(elem.Element("Sum").Value),
                        Status = (OrderStatus)Convert.ToInt32(elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = String.IsNullOrEmpty(elem.Element("DateImplement").Value) ? DateTime.MinValue : Convert.ToDateTime(elem.Element("DateImplement").Value)
                    });
                }
            }
            return list;
        }

        private List<IceCream> LoadIceCreams()
        {
            var list = new List<IceCream>();
            if (File.Exists(IceCreamFileName))
            {
                XDocument xDocument = XDocument.Load(IceCreamFileName);
                var xElements = xDocument.Root.Elements("IceCream").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in elem.Element("IceCreamComponents").Elements("IceCreamComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new IceCream
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IceCreamName = elem.Element("IceCreamName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        IceCreamComponents = prodComp
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach(var client in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(client.Attribute("Id").Value),
                        ClientFIO = client.Element("ClientFIO").Value,
                        Email = client.Element("Email").Value,
                        Password = client.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
                    });
                }
            }
            return list;
        }

        private void SaveMessageInfos()
        {
            if (MessageInfos != null)
            {
                var xElement = new XElement("MessageInfo");
                foreach (var messageInfo in MessageInfos)
                {
                    xElement.Add(new XElement("MessageInfo",
                    new XAttribute("MessageId", messageInfo.MessageId),
                    new XElement("Subject", messageInfo.Subject),
                    new XElement("SenderName", messageInfo.SenderName),
                    new XElement("Body", messageInfo.Body),
                    new XElement("ClientId", messageInfo.ClientId),
                    new XElement("DateDelivery", messageInfo.DateDelivery)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MailsFileName);
            }
        }

        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                XDocument xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var whComp = new Dictionary<int, int>();
                    foreach (var component in elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        whComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WarehouseName = elem.Element("WarehouseName").Value,
                        ResponsiblePerson = elem.Element("ResponsiblePerson").Value,
                        CreateDate = Convert.ToDateTime(elem.Element("CreateDate").Value),
                        WarehouseComponents = whComp
                    });
                }
            }
            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("IceCreamId", order.IceCreamId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", (int)order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveIceCreams()
        {
            if (IceCreams != null)
            {
                var xElement = new XElement("IceCreams");
                foreach (var iceCream in IceCreams)
                {
                    var compElement = new XElement("IceCreamComponents");
                    foreach (var component in iceCream.IceCreamComponents)
                    {
                        compElement.Add(new XElement("IceCreamComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("IceCream",
                     new XAttribute("Id", iceCream.Id),
                     new XElement("IceCreamName", iceCream.IceCreamName),
                     new XElement("Price", iceCream.Price),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(IceCreamFileName);
            }
        }

        private void SaveClients()
        {
            if(Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach(var client in Clients)
                {
                    xElement.Add(new XElement("Client"),
                        new XAttribute("Id", client.Id),
                        new XElement("ClientFIO", client.ClientFIO),
                        new XElement("Email", client.Email),
                        new XElement("Password", client.Password));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }

        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                    new XAttribute("Id", warehouse.Id),
                    new XElement("WarehouseName", warehouse.WarehouseName),
                    new XElement("ResponsiblePerson", warehouse.ResponsiblePerson),
                    new XElement("CreateDate", warehouse.CreateDate),
                    compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
