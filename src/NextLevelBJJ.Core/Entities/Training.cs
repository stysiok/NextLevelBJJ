using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class Training
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan FinishHour { get; private set; }
        public bool IsKidsTraining { get; set; }
    }
}
