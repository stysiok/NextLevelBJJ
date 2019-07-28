using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class Pass
    {
        public Guid Id { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public Guid StudentId { get; private set; }
        public int Price { get; private set; }
        public Guid TypeId { get; private set; }

        public Student Student { get; private set; }
        public PassType Type { get; private set; }
        public ICollection<Attendance> Attendances { get; private set; }
    }
}
