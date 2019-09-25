using System;
namespace NextLevelBJJ.Application.Passes.DTO
{
    public class PassDto
    {

        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid StudentId { get; set; }
        public int Price { get; set; }
        public Guid TypeId { get; set; }

        //public StudentDto Student { get; set; }
        //public PassTypeDto PassType { get; set; }
        //public ICollection<AttendanceDto> Attendances { get; set; }

        public PassDto()
        {
        }
    }
}
