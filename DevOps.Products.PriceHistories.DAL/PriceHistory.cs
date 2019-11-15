﻿using System;
using DevOps.Products.Common.Repository;

namespace DevOps.Products.PriceHistories.DAL
{
    public class PriceHistory : Entity
    {
        public decimal Price { get; set; }
        public DateTime DateArchived { get; set; }

        public int ProductID { get; set; }
    }
}
