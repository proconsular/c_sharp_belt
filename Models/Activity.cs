using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Belt.Models {
    public class EventActivity {
        public int id { get; set; }
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime date { get; set; }
        public DateTime time { get; set ;}
        public float duration { get; set; }
        public string description { get; set; }
        public int creatorId { get; set; }
        public User creator { get; set; }
        public List<Participant> participants { get; set; }

        public EventActivity() {
            participants = new List<Participant>();
        }
    }
}