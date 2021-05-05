using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StopHunger.Models
{
    [NotMapped]// don't add to DB
    public class LoginUser
    {
        [Required(ErrorMessage = "is required")]
        [MinLength(4, ErrorMessage = "must be at least 4 characters")]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }
        // *************************Password********************\\
        [Required(ErrorMessage = "is required")]
        [MinLength(8, ErrorMessage = "must be at least 8characters")]
        [DataType(DataType.Password)]//auto fills input type attr
        public string LoginPassword { get; set; }
        // checking to see if github is working
    }
}