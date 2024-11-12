using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2SD_DemoEF
{
    public class Student
    {
        [Key, RegularExpression(@"\d{7}")]
        [Column(TypeName = "char(7)")]
        public string StudentID { get; set; } = string.Empty;
        [Required,MaxLength(25)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(15)]
        public string? MiddleName { get; set; } = null;
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; } = DateTime.MinValue;
        public List<Result> Results { get; set; } = new List<Result>();
        public string? SlbID { get; set; } = null;
        public Teacher? Slb { get; set; } = null;

        public override string ToString()
        {
            var fullName = MiddleName == null ? $"{LastName}, {FirstName}" : 
                $"{LastName}, {FirstName} {MiddleName}";
            fullName += $" ({StudentID})";
            return fullName;
        }
    }
}
