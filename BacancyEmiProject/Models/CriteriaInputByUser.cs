using System;
using System.ComponentModel.DataAnnotations;

namespace BacancyEmiProject.Models
{
    public class CriteriaInputByUser
    {
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal LoanAmount { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "Opening length can't be more than 3.")]
        [Range(0, 100)]
        public decimal LoanInterest { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "Opening length can't be more than 5.")]
        [Range(0,20)]
        public decimal NoOfYear { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal MonthlyEmi { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Opening length can't be more than 16.")]
        [Range(0, 999999999999.999)]
        public decimal RateOfInterest { get; set; }
    }
}
