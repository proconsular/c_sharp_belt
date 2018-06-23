using System.Collections.Generic;
using System;

namespace Belt.Models {
    public class User {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime created_at { get; set; }
        public List<Participant> participants { get; set; }

        public User() {
            created_at = DateTime.Now;
            participants = new List<Participant>();
        }
    }
}