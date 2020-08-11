using System;
using System.ComponentModel.DataAnnotations;

namespace BacancyEmiProject.Models
{
    public class LoanEmiTransaction
    {
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal Opening { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal Principal { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal Interest { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal Emi { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal Closing { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal CummulativeInterest { get; set; }
    }
}
