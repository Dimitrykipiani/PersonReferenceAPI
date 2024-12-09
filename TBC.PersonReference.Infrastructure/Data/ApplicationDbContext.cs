using Microsoft.EntityFrameworkCore;
using TBC.PersonReference.Core.Entities;

namespace TBC.PersonReference.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.PrivateNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(p => p.BirthDate)
                    .IsRequired();

                entity.Property(p => p.Image)
                    .HasMaxLength(255);

                entity.Property(p => p.CreatedAt)
                    .IsRequired();

                entity.HasMany(p => p.PhoneNumbers);

                entity.HasMany(p => p.RelatedPersons)
                    .WithOne(pr => pr.Person)
                    .HasForeignKey(pr => pr.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasKey(pn => pn.Id);

            });

            modelBuilder.Entity<PersonRelation>(entity =>
            {
                entity.HasKey(pr => pr.Id);

                // Configure self-referencing relationship for PersonRelations
                entity.HasOne(pr => pr.Person)
                    .WithMany(p => p.RelatedPersons)
                    .HasForeignKey(pr => pr.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pr => pr.RelatedPerson)
                    .WithMany()
                    .HasForeignKey(pr => pr.RelatedPersonId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(pr => pr.RelatedPersonId)
                    .IsRequired();
            });
        }
    }
}
