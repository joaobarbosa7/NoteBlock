using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace NoteBlock.Infrastructure
{
    public class NoteBLockDbContext : DbContext
    {
        public NoteBLockDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Combine(path, "NoteBlock.db");
        }
        public string DbPath { get;  set; }

        public DbSet<Note> Notes { get;  set; }
        public DbSet<Reminder> Reminders { get;  set;}
        public DbSet<NoteCategory> NoteCategories { get;  set;}
        public DbSet<ReminderCategory> ReminderCategories { get;  set; }

        public DbSet<Category> Categories { get;  set; }
        public DbSet<AdminUser> AdminUsers { get;  set; }
        public DbSet<CommonUser> CommonUsers { get;  set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source = {DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().Property(x => x.Title).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Reminder>().Property(x => x.Title).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<AdminUser>().Property(x => x.Name).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<AdminUser>().Property(x => x.Email).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<AdminUser>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<AdminUser>().Property(x => x.Password).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<AdminUser>().Property(x => x.EmployeeNumber).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<CommonUser>().Property(x => x.Name).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<CommonUser>().Property(x => x.Email).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<CommonUser>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<CommonUser>().Property(x => x.Password).IsRequired().HasMaxLength(256);
            
            modelBuilder.Entity<NoteCategory>().HasKey(sc => new { sc.NoteId, sc.CategoryId });
            modelBuilder.Entity<ReminderCategory>().HasKey(sc => new { sc.ReminderId, sc.CategoryId });
        }


    }
}