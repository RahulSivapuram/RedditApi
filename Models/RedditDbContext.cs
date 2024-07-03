using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RedditApi.Models;

public partial class RedditDbContext : DbContext
{
    public RedditDbContext()
    {
    }

    public RedditDbContext(DbContextOptions<RedditDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Postdetail> Postdetails { get; set; }

    public virtual DbSet<Subreddit> Subreddits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-L4KPIVB;Database=reddit;Trust Server Certificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83FDD873468");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.PostId).HasColumnName("post_Id");
            entity.Property(e => e.UserId).HasColumnName("user_Id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__comments__post_I__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__comments__user_I__3F466844");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__posts__3213E83FD66DD8DB");

            entity.ToTable("posts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.SubredditId).HasColumnName("subreddit_Id");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_At");
            entity.Property(e => e.Url)
                .HasMaxLength(256)
                .HasColumnName("url");
            entity.Property(e => e.UserId).HasColumnName("user_Id");

            entity.HasOne(d => d.Subreddit).WithMany(p => p.Posts)
                .HasForeignKey(d => d.SubredditId)
                .HasConstraintName("FK__posts__subreddit__3B75D760");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__posts__user_Id__3C69FB99");
        });

        modelBuilder.Entity<Postdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("postdetail");

            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Subreddit)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("subreddit");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_At");
            entity.Property(e => e.Url)
                .HasMaxLength(256)
                .HasColumnName("url");
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Subreddit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subreddi__3213E83F6BCC4887");

            entity.ToTable("subreddit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F5C723534");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
