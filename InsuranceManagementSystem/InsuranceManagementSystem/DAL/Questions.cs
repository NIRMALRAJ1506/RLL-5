using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }

        [MaxLength(255)]
        public string Question { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Answer { get; set; }


        public int CustomerId { get; set; }

        // Navigation property to represent the relationship

    }
}
