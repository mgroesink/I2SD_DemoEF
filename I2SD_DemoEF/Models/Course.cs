using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2SD_DemoEF
{
    public class Course
    {
        private string _courseID = string.Empty;

        [Key, RegularExpression(@"[A-Z]{3}")]
        [MaxLength(3)]
        public string CourseID
        {
            get => _courseID;
            set => _courseID = value.ToUpper();
        }
        [Required, MaxLength(75)]
        public string CourseName { get; set; } = string.Empty;
        public List<Result> Results { get; set; } = new List<Result>();
    }
}
