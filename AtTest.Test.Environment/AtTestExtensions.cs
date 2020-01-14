﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signum.Engine;
using Signum.Entities;
using Signum.Utilities;
using AtTest.Entities;

namespace AtTest.Test.Environment
{
    public static class AtTestExtensions
    {
        public static OrderDetailEmbedded AddLine(this OrderEntity order, string productName, int quantity = 1, decimal discount = 0)
        {
            var product = Database.Query<ProductEntity>().SingleEx(p => p.ProductName.Contains(productName));

            return AddLine(order, product, quantity, discount);
        }

        public static OrderDetailEmbedded AddLine(this OrderEntity order, ProductEntity product, int quantity = 1, decimal discount = 0)
        {
            var result = new OrderDetailEmbedded
            {
                Product = product.ToLite(),
                UnitPrice = product.UnitPrice,
                Quantity = quantity,
                Discount = discount,
            };

            order.Details.Add(result);

            return result;
        }
    }
}
