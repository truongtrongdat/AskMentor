namespace Libs
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Evaluate> Evaluates { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Money> Moneys { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<RoomTopic> RoomTopic { get; set; }
        public DbSet<MessageInRoomTopic> MessageInRoomTopic { get; set; }
    }
}