﻿using StoreManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public PayStatus PayStatus { get; set; }
        public DateTime CreateAt { get; set; }
    }
}