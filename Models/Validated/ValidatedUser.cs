using System.ComponentModel.DataAnnotations;

namespace Belt.Models {
    public class ValidatedUser {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        [Display(Name = "First Name")]
        [MinLength(3)]
        public string first_name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        [Display(Name = "Last Name")]
        [MinLength(3)]
        public string last_name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*#?&])[A-Za-z\\d$@$!%*#?&]+$", ErrorMessage = "Password must contain 8 characters and 1 uppercase, 1 digit, and 1 special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(8)]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string confirm_password { get; set; }
    }
}