﻿using IceCreamShopListImplement.Models;
using System.Collections.Generic;

namespace IceCreamShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Component> Components { get; set; }

        public List<Order> Orders { get; set; }

        public List<IceCream> IceCreams { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            IceCreams = new List<IceCream>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
