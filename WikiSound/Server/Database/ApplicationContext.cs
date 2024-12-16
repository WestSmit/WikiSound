using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WikiSound.Server.Models;

namespace WikiSound.Server.Database
{
    internal class ApplicationContext : IdentityUserContext<IdentityUser>
    {
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Track> Tracks => Set<Track>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Artist>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Album>()
    .           HasKey(a => a.Id);

            modelBuilder.Entity<Album>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Album>()
                .HasOne(a => a.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Track>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Track>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Track>()
                .HasOne(a => a.Artist)
                .WithMany(a => a.Tracks)
                .HasForeignKey(t => t.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Track>()
                .HasOne(t => t.Album)
                .WithMany(a => a.Tracks)
                .HasForeignKey(t => t.AlbumId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
