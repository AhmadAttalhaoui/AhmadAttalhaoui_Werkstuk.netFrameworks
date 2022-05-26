using System.ComponentModel.DataAnnotations;

namespace API3.Models
{
    public class Critique
    {
        [Key]
        public int Id { get; set; }
        public String imdbID { get; set; }
        public String Content { get; set; }
        public Search search { get; set; }
    }
}
