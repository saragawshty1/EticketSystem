using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MOVIES.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MOVIES.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(1, 500)]
        public double Price { get; set; }
        //[RegularExpression(@"movie[0-9]+\.(jpg|png)", ErrorMessage = "your photos must be suffix with .png or .jpg")]
        public string ImgUrl { get; set; }
        [Required]
        public string TrailerUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public MovieStatus MovieStatus { get; set; }
        [ValidateNever]
        public int viewcount { get; set; }
        public int CategoryId { get; set; }
        public int CinemaId { get; set; }
        [ValidateNever]
        public Category category { get; set; }
        [ValidateNever]
        public Cinema cinema { get; set; }
        [ValidateNever]
        public List<Actor> Actors{ get; set; }
        //[ValidateNever]
        //public List<ActorMovie> ActorMovie{ get; set; }
    }
}
