using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment2.Models
{
    public partial class Assignment2Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-R9FQAAL\SQLEXPRESS;Database=Assignment2;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cineplex>(entity =>
            {
                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();
            });

            modelBuilder.Entity<CineplexMovie>(entity =>
            {
                entity.HasKey(e => new { e.CineplexId, e.MovieId })
                    .HasName("PK__Cineplex__CB419E6D69413F80");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Cinep__2B3F6F97");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Movie__2C3393D0");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__CineplexM__Sessi__398D8EEE");
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.Property(e => e.EnquiryID).HasColumnName("EnquiryID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<MovieComingSoon>(entity =>
            {
                entity.Property(e => e.MovieComingSoonId).HasColumnName("MovieComingSoonID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<SessionTime>(entity =>
            {
                entity.Property(e => e.SessionTimeId).HasColumnName("SessionTimeID");

                entity.Property(e => e.SessionTime1).HasColumnType("datetime");

                entity.Property(e => e.SessionTime2).HasColumnType("datetime");
            });
        }

        public virtual DbSet<Cineplex> Cineplex { get; set; }
        public virtual DbSet<CineplexMovie> CineplexMovie { get; set; }
        public virtual DbSet<Enquiry> Enquiry { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieComingSoon> MovieComingSoon { get; set; }
        public virtual DbSet<SessionTime> SessionTime { get; set; }
    }
}