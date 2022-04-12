﻿using IceCreamShopContracts.Enums;
using System;

namespace IceCreamShopListImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int IceCreamId { get; set; }

        public int ClientId { get; set; }

        public int? ImplementerId { get; set; }

        public string ClientFIO { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
