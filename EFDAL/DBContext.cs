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
        public virtual DbSet<BirthDay> Days { get; set; }
        public virtual DbSet<Quicklist> Quicklists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region Person settings

            modelBuilder.Entity<Person>().Property(p => p.Email)
                                         .IsRequired()
                                         .HasMaxLength(100)
                                         .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                          new IndexAnnotation(new IndexAttribute("IX_Email", 1) { IsUnique = true }));
            modelBuilder.Entity<Person>().Property(p => p.Password)
                                         .IsRequired()
                                         .HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(p => p.FirstName)
                                         .HasMaxLength(100);
            modelBuilder.Entity<Person>().Property(p => p.LastName)
                                         .HasMaxLength(100);
            #endregion

            #region Phone settings

            modelBuilder.Entity<Phone>().Property(p => p.Number)
                                        .HasMaxLength(50);
            #endregion

            #region Photo settings

            modelBuilder.Entity<Photo>().Property(p => p.ImageMimeType)
                                        .HasMaxLength(50);
            //Photo one-to-one settings
            modelBuilder.Entity<Person>().HasOptional(p => p.Photo)
                            .WithRequired(ph => ph.Person).WillCascadeOnDelete();

            #endregion

            #region BirtDay settings

            modelBuilder.Entity<BirthDay>().Property(p => p.Date)
                                         .HasColumnType("Date");
            //BirthDay one-to-one settings
            modelBuilder.Entity<Person>().HasOptional(p => p.BirthDay)
                            .WithRequired(d => d.Person).WillCascadeOnDelete();
            #endregion

            #region Quicklist settings

            //Quiclist settings 
            modelBuilder.Entity<Quicklist>().Property(p => p.Name)
                                       .HasMaxLength(30);
            modelBuilder.Entity<Quicklist>().Property(p => p.QuicklistId)
                                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            #endregion
        }
    }
}