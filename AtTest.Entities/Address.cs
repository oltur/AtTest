﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Entities;
using System.Reflection;
using Signum.Utilities;

namespace AtTest.Entities
{
    [Serializable]
    public class AddressEmbedded : EmbeddedEntity
    {
        [StringLengthValidator(Min = 3, Max = 60, MultiLine = true)]
        public string Address { get; set; }

        [StringLengthValidator(Min = 3, Max = 15)]
        public string City { get; set; }

        [StringLengthValidator(Min = 2, Max = 15)]
        public string? Region { get; set; }

        [StringLengthValidator(Min = 3, Max = 10)]
        public string? PostalCode { get; set; }

        [StringLengthValidator(Min = 2, Max = 15)]
        public string Country { get; set; }

        protected override string? PropertyValidation(PropertyInfo pi)
        {
            if (pi.Name == nameof(PostalCode))
            {
                if (string.IsNullOrEmpty(PostalCode) && Country != "Ireland")
                    return Signum.Entities.ValidationMessage._0IsNotSet.NiceToString().FormatWith(pi.NiceName());
            }

            return null;
        }

        public override string ToString()
        {
            return "{0}\r\n{1} {2} ({3})".FormatWith(Address, PostalCode, City, Country);
        }

        public AddressEmbedded Clone()
        {
            return new AddressEmbedded
            {
                Address = this.Address,
                City = this.City,
                Region = this.Region,
                PostalCode = this.PostalCode,
                Country = this.Country
            };
        }
    }
}
