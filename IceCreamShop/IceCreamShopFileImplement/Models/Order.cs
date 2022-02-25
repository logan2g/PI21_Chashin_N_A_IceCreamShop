﻿using IceCreamShopContracts.Enums;
using System;

namespace IceCreamShopFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int IceCreamId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}