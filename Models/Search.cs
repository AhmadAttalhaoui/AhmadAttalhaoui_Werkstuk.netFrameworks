﻿using System.ComponentModel.DataAnnotations;

namespace API3.Models
{
    public class Search
    {
        [Key]
        public string imdbID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public string Runtime { get; set; }

        public ICollection<Critique>? Critiques { get; set; }
    }
}
