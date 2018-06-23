namespace Belt.Models {
    public class Participant {
        public int id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int activityId { get; set; }
        public EventActivity activity { get; set; }
    }
}