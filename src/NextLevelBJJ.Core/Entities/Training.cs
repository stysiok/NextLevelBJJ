using System;
using System.Collections.Generic;
using System.Text;
using NextLevelBJJ.Core.Entities.Extensions;

namespace NextLevelBJJ.Core.Entities
{
    public class Training : IActiveField, IAuditFields
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan FinishHour { get; private set; }
        public bool IsKidsTraining { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Training()
        {
            Attendances = new HashSet<Attendance>();
        }
    }
}
