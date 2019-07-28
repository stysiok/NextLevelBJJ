using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Commands
{
    public class CreatePassType : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Entries { get; set; }
        public bool IsOpen { get; set; }

        public CreatePassType(Guid id, string name, decimal price, int entries, bool isOpen)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            Price = price;
            Entries = entries;
            IsOpen = isOpen;
        }
    }
}
