using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string Role { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PassCode { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool HasDeclaration { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }
        public ICollection<Pass> Passes { get; private set; }
    }
}
