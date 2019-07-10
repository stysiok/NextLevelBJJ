using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class PassType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int Entries { get; private set; }
        public bool IsOpen { get; private set; }
    }
}
