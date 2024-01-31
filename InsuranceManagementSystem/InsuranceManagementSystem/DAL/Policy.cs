using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public enum PolicyType
    {
        Premium,
        Normal
    }
    public class Policy
    {


        [Key]
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "Policy Number is required")]
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "Applied Date is required")]
        [DataType(DataType.Date)]
        public DateTime AppliedDate { get; set; }

        // Example: Policy Type (Life, Health, Auto, etc.)
        public string Category { get; set; }


        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Policy Type is required")]
        public PolicyType PolicyType { get; set; }

        public double Price { get; set; }


    }
}
