namespace EFDAL
{
    using IDAL.Entities;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Linq;

    public class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext")
        {

        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //one to one
            modelBuilder.Entity<Person>().HasOptional(p => p.Photo)
                                         .WithRequired(ph => ph.Person);

            modelBuilder.Entity<Person>().Property(p => p.Password)
                                         .IsRequired();

            modelBuilder.Entity<Person>().Property(p => p.Email)
                                         .IsRequired()
                                         .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                          new IndexAnnotation(new IndexAttribute("IX_Email", 1) { IsUnique = true }));



        }
    }
}