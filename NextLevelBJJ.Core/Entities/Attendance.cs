using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class Attendance
    {
        public Guid Id { get; private set; }
        public Guid PassId { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid TrainingId { get; private set; }
        public bool IsFree { get; private set; }

        public Pass Pass { get; private set; }
        public Student Student { get; private set; }
    }
}
