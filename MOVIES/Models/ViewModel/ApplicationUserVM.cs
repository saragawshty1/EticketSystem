using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MOVIES.Models.ViewModel
{
    public class ApplicationUserVM
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]           
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [ValidateNever]
        public string Role { get; set; }
    }
}
