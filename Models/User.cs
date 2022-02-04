using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API3.Models
{
    public class User : IdentityUser
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        [Required]
        [Display(Name = "Geboren op")]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
    }

    public class UserIndexViewModel
    {
        public int SelectedItem { get; set; }
        public string SearchField { get; set; }
        public List<User> Users { get; set; }

    }
}
