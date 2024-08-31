using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Stripe.Climate;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOVIES.Models
{
    public class MovieCart
    {

        public int Id { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        [ValidateNever]
        public Movie Movie { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int Count { get; set; }
        public int? orderId { get; set; }
        public string? status { get; set; }

        //[ForeignKey("orderId")]
        //[ValidateNever]
        //public order order { get; set; }
    }
    }

