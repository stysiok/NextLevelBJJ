using NextLevelBJJ.Core.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Core.Entities
{
    public class Student : IActiveField, IAuditFields
    {
        public Guid Id { get; private set; }
        public string Role { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid PassCode { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Address { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool HasDeclaration { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }
        public ICollection<Pass> Passes { get; private set; }

        private Student()
        {
            Attendances = new HashSet<Attendance>();
            Passes = new HashSet<Pass>();
        }

        public Student(Guid id, string role, string firstName, string lastName, Guid passCode, Gender gender, DateTime birthDate, string address, int phoneNumber, string email, bool hasDeclaration)
        {
            Id = Guid.Empty == id ? Guid.NewGuid() : id;
            Role = string.IsNullOrWhiteSpace(role) ? "student" : role;
            SetFirstName(firstName);
            SetLastName(lastName);
            PassCode = Guid.Empty == PassCode ? Guid.NewGuid() : passCode;
            Gender = gender;
            BirthDate = birthDate;
            SetAddress(address);
            SetPhoneNumber(phoneNumber);
            SetEmail(email);
            HasDeclaration = hasDeclaration;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));

            FirstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

            LastName = lastName;
        }

        private void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty.", nameof(address));

            Address = address;
        }

        private void SetPhoneNumber(int phoneNumber)
        {
            if (phoneNumber.ToString().Length < 9)
                throw new ArgumentException("Value provided is not a phone number", nameof(phoneNumber));

            PhoneNumber = phoneNumber;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            Email = email;
        }
    }
}
