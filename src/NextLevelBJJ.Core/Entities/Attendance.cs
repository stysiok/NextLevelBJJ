using System;
using System.Collections.Generic;
using System.Text;
using NextLevelBJJ.Core.Entities.Extensions;

namespace NextLevelBJJ.Core.Entities
{
    public class Attendance : IActiveField, IAuditFields
    {
        public Guid Id { get; private set; }
        
        public bool IsFree { get; private set; }

        public Pass Pass { get; private set; }
        public Student Student { get; private set; }
        public Training Training { get; private set; }
    }
}
