using System.ComponentModel.DataAnnotations;

namespace API3.Models
{
    public class ApiFilm
    {
        [Key]
        public int ApiFilmId { get; set; }
        public string imdbID { get; set; }
        [Required]
        [Display(Name = "Film")]
        public Search Film { get; set; }

        public int UserId { get; set; }
        [Required]
        [Display(Name = "User")]
        public User User { get; set; }
    }
}
