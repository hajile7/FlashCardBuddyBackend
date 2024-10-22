using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlashCardBuddy_API.Classes;

namespace FlashCardBuddy_API.Models;

public partial class FlashCardBuddyDbContext : DbContext
{
    public FlashCardBuddyDbContext()
    {
    }

    public FlashCardBuddyDbContext(DbContextOptions<FlashCardBuddyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Secret.url);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.Flashcardid).HasName("PK__FLASHCAR__3BFFD39FA48566E2");

            entity.ToTable("FLASHCARDS");

            entity.Property(e => e.Flashcardid).HasColumnName("FLASHCARDID");
            entity.Property(e => e.Answer)
                .HasMaxLength(1000)
                .HasColumnName("ANSWER");
            entity.Property(e => e.Question)
                .HasMaxLength(500)
                .HasColumnName("QUESTION");
            entity.Property(e => e.Stack)
                .HasMaxLength(50)
                .HasColumnName("STACK");
            entity.Property(e => e.Userid).HasColumnName("USERID");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__User__7B9E7F35CF81956C");

            entity.ToTable("User");

            entity.Property(e => e.Userid).HasColumnName("USERID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("('1')");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(25)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(40)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("USERNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
