﻿using System;
using Signum.Engine;
using Signum.Engine.Authorization;
using Signum.Entities;
using Signum.Utilities;
using Signum.React.Selenium;
using AtTest.Entities;
using AtTest.Test.Environment;
using Xunit;

namespace AtTest.Test.React
{
    public class OrderReactTest : AtTestTestClass
    {
        public OrderReactTest()
        {
            AtTestEnvironment.StartAndInitialize();
            AuthLogic.GloballyEnabled = false;
        }

        [Fact]
        public void OrderWebTestExample()
        {
            Browse("Normal", b =>
            {
                Lite<OrderEntity>? lite = null;
                try
                {
                    b.SearchPage(typeof(PersonEntity)).Using(persons =>
                    {
                        persons.Search();
                        persons.SearchControl.Results.OrderBy("Id");
                        return persons.Results.EntityClick<PersonEntity>(1);
                    }).Using(john =>
                    {
                        using (FrameModalProxy<OrderEntity> order = john.ConstructFrom(OrderOperation.CreateOrderFromCustomer))
                        {
                            order.ValueLineValue(a => a.ShipName, Guid.NewGuid().ToString());
                            order.EntityCombo(a => a.ShipVia).SelectLabel("FedEx");

                            ProductEntity sonicProduct = Database.Query<ProductEntity>().SingleEx(p => p.ProductName.Contains("Sonic"));

                            var line = order.EntityDetail(a => a.Details).GetOrCreateDetailControl<OrderDetailEmbedded>();
                            line.EntityLineValue(a => a.Product, sonicProduct.ToLite());

                            Assert.Equal(sonicProduct.UnitPrice, order.ValueLineValue(a => a.TotalPrice));

                            order.Execute(OrderOperation.Save);

                            lite = order.GetLite();

                            Assert.Equal(sonicProduct.UnitPrice, order.ValueLineValue(a => a.TotalPrice));
                        }

                        return b.NormalPage(lite);

                    }).EndUsing(order =>
                    {
                        Assert.Equal(lite!.InDB(a => a.TotalPrice), order.ValueLineValue(a => a.TotalPrice));
                    });
                }
                finally
                {
                    if (lite != null)
                        lite.Delete();
                }
            });
        }//OrderReactTestExample
    }
}
