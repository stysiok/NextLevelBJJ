﻿using NextLevelBJJ.Core.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class PassType : IAuditFields, IActiveField
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Entries { get; private set; }
        public bool IsOpen { get; private set; }
        public ICollection<Pass> Passes { get; private set; }

        private PassType()
        {
            Passes = new HashSet<Pass>();
        }

        public PassType(Guid id, string name, decimal price, int entries, bool isOpen)
        {
            Id = Guid.Empty == id ? Guid.NewGuid() : id;
            SetName(name);
            SetPrice(price);
            SetEntries(entries);
            IsOpen = isOpen;
        }
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty", nameof(name));
            }

            Name = name;
        }

        private void SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be lower than 0", nameof(price));
            }

            Price = price;
        }

        private void SetEntries(int entries)
        {
            if (entries < 0)
            {
                throw new ArgumentException("Entries number cannot be lower than 0", nameof(entries));
            }

            Entries = entries;
        }
    }
}
