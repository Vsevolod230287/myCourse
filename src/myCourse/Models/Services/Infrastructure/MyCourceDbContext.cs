using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using myCourse.Models.Entities;

namespace myCourse.Models.Services.Infrastructure
{
    public partial class MyCourceDbContext : DbContext
    {


        public MyCourceDbContext(DbContextOptions<MyCourceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Course>(entity =>
            {

                entity.ToTable("Courses");   //superfluo se la proprietà, si chiama come la tabella

                entity.HasKey(course => course.Id);  //superfluo se la proprietà si chiama id oppure CoursesId come il nome della classe
                                                     //entity.HasKey(course => new { course.Id, course.Author })

                //Mapping per gli owned types
                entity.OwnsOne(course => course.CurrentPrice, builder =>
             {
                   builder.Property(money => money.Currency)
                   .HasConversion<string>()
                   .HasColumnName("CurrentPrice_Currency");//superfluo perchè le nostre colonne seguona già la convenzione dei nomi
                   builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount");//superfluo perchè le nostre colonne seguona già la convenzione dei nomi
               });

                entity.OwnsOne(course => course.FullPrice, builder =>
             {
                   builder.Property(money => money.Currency).HasConversion<string>();
               });

                //Mapping per le Relazioni
                entity.HasMany(course => course.Lessons)
                   .WithOne(lesson => lesson.Course)
                   .HasForeignKey(lesson => lesson.CourseId);//supeflua se la proprietà si chiama CourseId

                #region Mapping generato automaticamente dal tool ReverseEngineering
                /*
                 entity.Property(e => e.Id).ValueGeneratedNever();

                 entity.Property(e => e.Author)
                     .IsRequired()
                     .HasColumnType("TEXT (100)");

                 entity.Property(e => e.CurrentPriceAmount)
                     .IsRequired()
                     .HasColumnName("CurrentPrice_Amount")
                     .HasColumnType("NUMERIC")
                     .HasDefaultValueSql("0");

                 entity.Property(e => e.CurrentPriceCurrency)
                     .IsRequired()
                     .HasColumnName("CurrentPrice_Currency")
                     .HasColumnType("TEXT(3)")
                     .HasDefaultValueSql("'EUR'");

                 entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                 entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                 entity.Property(e => e.FullPriceAmount)
                     .IsRequired()
                     .HasColumnName("FullPrice_Amount")
                     .HasColumnType("NUMERIC")
                     .HasDefaultValueSql("0");

                 entity.Property(e => e.FullPriceCurrency)
                     .IsRequired()
                     .HasColumnName("FullPrice_Currency")
                     .HasColumnType("TEXT(3)")
                     .HasDefaultValueSql("'EUR'");

                 entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                 entity.Property(e => e.Title)
                     .IsRequired()
                     .HasColumnType("TEXT (100)");
                     */
                #endregion


            });



            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(lesson => lesson.Course)
                   .WithMany(course => course.Lessons);

                #region Mapping generato automaticamente dal tool ReverseEngineering
                //    entity.Property(e => e.Id).ValueGeneratedNever();

                //    entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                //    entity.Property(e => e.Duration)
                //           .IsRequired()
                //           .HasColumnType("TEXT (8)")
                //           .HasDefaultValueSql("'00:00:00'");

                //    entity.Property(e => e.Title)
                //           .IsRequired()
                //           .HasColumnType("TEXT (100)");

                //    entity.HasOne(d => d.Course)
                //           .WithMany(p => p.Lessons)
                //           .HasForeignKey(d => d.CourseId);
                #endregion
            });
        }
    }
}
