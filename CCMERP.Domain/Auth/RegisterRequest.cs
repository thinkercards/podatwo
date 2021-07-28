using System.ComponentModel.DataAnnotations;

namespace CCMERP.Domain.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string role { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string phoneNumber { get; set; }

       
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int mapId { get; set; }


        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Org_ID { get; set; }
        

    }
}
