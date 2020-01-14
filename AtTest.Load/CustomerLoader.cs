﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Engine;
using AtTest.Entities;
using Signum.Utilities;
using Signum.Entities;
using Signum.Services;
using System.Globalization;
using AtTest.Load.NorthwindSchema;

namespace AtTest.Load
{
    internal static class CustomerLoader
    {
        public static void LoadCompanies()
        {
            var companies = Connector.Override(Northwind.Connector).Using(_ =>
                Database.View<Customers>()
                .Where(c => !c.ContactTitle.Contains("Owner"))
                .ToList());

            companies.Select(c => new CompanyEntity
            {
                CompanyName = c.CompanyName,
                ContactName = c.ContactName,
                ContactTitle = c.ContactTitle,
                Address = new AddressEmbedded
                {
                    Address = c.Address,
                    City = c.City,
                    Region = c.Region,
                    PostalCode = c.PostalCode,
                    Country = c.Country,
                },
                Phone = c.Phone.Replace(".", " "),
                Fax = c.Fax == null ? null : c.Fax.Replace(".", " "),
            }).SaveList();

        }

        public static void LoadPersons()
        {
            var persons = Connector.Override(Northwind.Connector).Using(_ =>
                Database.View<Customers>()
                .Where(c => c.ContactTitle.Contains("Owner"))
                .ToList());

            persons.Select(c => new PersonEntity
            {
                FirstName = c.ContactName.Substring(0, c.ContactName.LastIndexOf(' ')),
                LastName = c.ContactName.Substring(c.ContactName.LastIndexOf(' ') + 1),
                DateOfBirth = null,
                Title = null,
                Address = new AddressEmbedded
                {
                    Address = c.Address,
                    City = c.City,
                    Region = c.Region,
                    PostalCode = c.PostalCode,
                    Country = c.Country,
                },
                Phone = c.Phone.Replace(".", " "),
                Fax = c.Fax == null ? null : c.Fax.Replace(".", " "),
                Corrupt = true,
            }).SaveList();
        }
    }
}
