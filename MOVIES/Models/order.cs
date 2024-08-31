using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOVIES.Models
{
    public class order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; } // e.g., "Paid", "Pending"
        public ICollection<MovieCart> OrderItems { get; set; }
    }
}
