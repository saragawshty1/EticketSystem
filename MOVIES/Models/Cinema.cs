using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MOVIES.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string CinemaLogo { get; set; }
        [Required]
        public string Address { get; set; }
        [ValidateNever]
        public List<Movie> Movies { get; set; }
    }
}
