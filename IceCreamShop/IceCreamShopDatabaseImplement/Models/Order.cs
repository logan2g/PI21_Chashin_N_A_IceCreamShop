﻿using IceCreamShopContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IceCreamShopDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int IceCreamId { get; set; }

        public string OrderName { get; set; }

        public string IceCreamName { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}