using System.ComponentModel.DataAnnotations;
using System;

namespace Belt.Models {
    public class ValidatedEventActivity {
        [Required]
        [MinLength(2)]
        public string name { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime time { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [Required]
        public float duration { get; set; }
        [Required]
        public int scale { get; set; }
        public string description { get; set; }
    }
}