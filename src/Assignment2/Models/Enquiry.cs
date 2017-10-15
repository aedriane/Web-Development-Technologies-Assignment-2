using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Enquiry
    {
        public int EnquiryID { get; set; }

        [Required(ErrorMessage = "Please enter your Email Address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}

