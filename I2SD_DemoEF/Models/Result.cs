using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2SD_DemoEF
{
    
    [PrimaryKey(nameof(StudentID),nameof(CourseID), nameof(ResultDate))]
    public class Result
    {
        public string StudentID { get; set; } = string.Empty;
        public Student? Student { get; set; } = null!;
        public string CourseID { get; set; } = string.Empty;
        public Course? Course { get; set; } = null!;
        public string? TeacherID { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; } = null!;
        public double Score { get; set; }
        [DataType(DataType.Date)]
        public DateTime ResultDate { get; set; } = DateTime.MinValue;
    }
}
