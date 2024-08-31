using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace MOVIES.Models
{
    public class Category 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ValidateNever]
        public List<Movie> Movies { get; set; }    
    }
}
