using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class QuestionView
    {

        public int QuestionId { get; set; }

        //[Required(ErrorMessage = "Customer Name is required.")]
        //[MaxLength(100)] // Adjust the length as needed
        //public string CUSTNAME { get; set; }

        [MaxLength(255)] // Adjust the length as needed
        public string Question { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now; // Default to the current date

        [MaxLength(255)] // Adjust the length as needed
        public string Answer { get; set; }




        //

        [Required(ErrorMessage = "Customer Id is required.")]
        public int CustomerId { get; set; }
    }
}