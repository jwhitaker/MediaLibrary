using System.Data.Entity;

namespace MediaLibrary.Models
{
    public class MediaLibraryContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MediaLibrary.Models.MediaLibraryContext>());

        public MediaLibraryContext() : base("name=MediaLibraryContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Poster> Posters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .Map(x =>
                {
                    x.MapLeftKey("MovieId");
                    x.MapRightKey("GenreId");
                    x.ToTable("MovieGenres");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
