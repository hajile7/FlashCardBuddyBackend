using System;
using System.Collections.Generic;
using FlashCardBuddy_API.Classes;
using Microsoft.EntityFrameworkCore;

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
            entity.HasKey(e => e.Flashcardid).HasName("PK__FLASHCAR__3BFFD39FDE2DD7F8");

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

            entity.HasOne(d => d.User).WithMany(p => p.Flashcards)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__FLASHCARD__USERI__3A81B327");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__User__7B9E7F3508D23DB2");

            entity.ToTable("User");

            entity.Property(e => e.Userid).HasColumnName("USERID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("('1')");
            entity.Property(e => e.Firstname)
                .HasMaxLength(25)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(40)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
