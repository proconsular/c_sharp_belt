using Microsoft.EntityFrameworkCore;

namespace Belt.Models {
    public class StorageContext: DbContext {
        public DbSet<User> users { get; set; }
        public DbSet<EventActivity> activities { get; set; }
        public DbSet<Participant> participants { get; set; }
        public StorageContext(DbContextOptions<StorageContext> options) : base(options) {}
    }
}