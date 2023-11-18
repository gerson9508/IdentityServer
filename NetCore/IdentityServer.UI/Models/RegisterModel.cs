using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
   public class RegisterModel
   {
      [Required]
      public string Role { get; set; }
      [Required]
      [EmailAddress]
      public string Email { get; set; }
      public string PhoneNumber { get; set; }


      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm Password")]
      [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
      public string ConfirmPassword { get; set; }
   }
}
