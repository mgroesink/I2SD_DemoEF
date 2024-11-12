using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2SD_DemoEF
{
    public class Teacher
    {
        [Key, RegularExpression(@"[A-Z]{3}\d{2}")]
        [Column(TypeName = "char(5)")]
        public string TeacherID { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(15)]
        public string? MiddleName { get; set; } = null;
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public List<Result> Results { get; set; } = new List<Result>();

        public override string ToString()
        {
            var fullName = MiddleName == null ? $"{LastName}, {FirstName}" : 
                $"{LastName}, {FirstName} {MiddleName}";
            fullName += $" ({TeacherID})";
            return fullName;
        }
    }
}
