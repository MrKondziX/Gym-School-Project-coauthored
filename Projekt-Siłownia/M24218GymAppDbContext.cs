using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Projekt_Siłownia;

public partial class M24218GymAppDbContext : DbContext
{
    public M24218GymAppDbContext()
    {
    }

    public M24218GymAppDbContext(DbContextOptions<M24218GymAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carnet> Carnets { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExercisesMuscle> ExercisesMuscles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersCoach> UsersCoaches { get; set; }

    public virtual DbSet<UsersKlient> UsersKlients { get; set; }

    public virtual DbSet<UsersKlientCarnet> UsersKlientCarnets { get; set; }

    public virtual DbSet<UsersKlientTrening> UsersKlientTrenings { get; set; }

    public virtual DbSet<UsersKlientTreningplan> UsersKlientTreningplans { get; set; }

    public virtual DbSet<UsersType> UsersTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=mysql.ct8.pl;database=m24218_gym-app-db;user=m24218_GymAppAcc;Password=P@p!ez.2137$", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carnet>(entity =>
        {
            entity.HasKey(e => e.CarnetId).HasName("PRIMARY");

            entity.ToTable("carnets");

            entity.Property(e => e.CarnetId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("carnet_id");
            entity.Property(e => e.CarnetCost).HasColumnName("carnet_cost");
            entity.Property(e => e.CarnetName)
                .HasColumnType("text")
                .HasColumnName("carnet_name");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExsId).HasName("PRIMARY");

            entity.ToTable("exercises");

            entity.HasIndex(e => e.ExsMuscleId, "exercises_muscle_id");

            entity.Property(e => e.ExsId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("exs_id");
            entity.Property(e => e.ExsDescription)
                .HasColumnType("text")
                .HasColumnName("exs_description");
            entity.Property(e => e.ExsMuscleId)
                .HasColumnType("int(11)")
                .HasColumnName("exs_muscle_id");
            entity.Property(e => e.ExsName)
                .HasColumnType("text")
                .HasColumnName("exs_name");

            entity.HasOne(d => d.ExsMuscle).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.ExsMuscleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exercises_ibfk_1");
        });

        modelBuilder.Entity<ExercisesMuscle>(entity =>
        {
            entity.HasKey(e => e.ExsMuscleId).HasName("PRIMARY");

            entity.ToTable("exercises_muscles");

            entity.Property(e => e.ExsMuscleId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("exs_muscle_id");
            entity.Property(e => e.ExsMuscleName)
                .HasColumnType("text")
                .HasColumnName("exs_muscle_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UsersTypeId, "users_type_id");

            entity.Property(e => e.UsersId)
                .HasColumnType("int(11)")
                .HasColumnName("users_id");
            entity.Property(e => e.UsersEmail)
                .HasColumnType("text")
                .HasColumnName("users_email");
            entity.Property(e => e.UsersLogin)
                .HasColumnType("text")
                .HasColumnName("users_login");
            entity.Property(e => e.UsersName)
                .HasColumnType("text")
                .HasColumnName("users_name");
            entity.Property(e => e.UsersPassword)
                .HasMaxLength(255)
                .HasColumnName("users_password");
            entity.Property(e => e.UsersSurname)
                .HasColumnType("text")
                .HasColumnName("users_surname");
            entity.Property(e => e.UsersTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("users_type_id");

            entity.HasOne(d => d.UsersType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UsersTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<UsersCoach>(entity =>
        {
            entity.HasKey(e => e.UsersCoachId).HasName("PRIMARY");

            entity.ToTable("users_coach");

            entity.HasIndex(e => e.UsersId, "users_id");

            entity.Property(e => e.UsersCoachId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("users_coach_id");
            entity.Property(e => e.UsersCoachNott)
                .HasColumnType("text")
                .HasColumnName("users_coach_nott");
            entity.Property(e => e.UsersId)
                .HasColumnType("int(11)")
                .HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.UsersCoaches)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_coach_ibfk_1");
        });

        modelBuilder.Entity<UsersKlient>(entity =>
        {
            entity.HasKey(e => e.UsersKlientId).HasName("PRIMARY");

            entity.ToTable("users_klient");

            entity.HasIndex(e => e.UsersCoachId, "connect_coach");

            entity.HasIndex(e => e.UsersId, "users_id");

            entity.Property(e => e.UsersKlientId)
                .HasColumnType("int(11)")
                .HasColumnName("users_klient_id");
            entity.Property(e => e.UsersCoachId)
                .HasColumnType("int(11)")
                .HasColumnName("users_coach_id");
            entity.Property(e => e.UsersId)
                .HasColumnType("int(11)")
                .HasColumnName("users_id");

            entity.HasOne(d => d.UsersCoach).WithMany(p => p.UsersKlients)
                .HasForeignKey(d => d.UsersCoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_ibfk_2");

            entity.HasOne(d => d.Users).WithMany(p => p.UsersKlients)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_ibfk_1");
        });

        modelBuilder.Entity<UsersKlientCarnet>(entity =>
        {
            entity.HasKey(e => e.UsersKlientCarnetId).HasName("PRIMARY");

            entity.ToTable("users_klient_carnets");

            entity.HasIndex(e => e.CarnetId, "carnet_id");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.UsersKlientCarnetId)
                .HasColumnType("int(11)")
                .HasColumnName("users_klient_carnet_id");
            entity.Property(e => e.CarnetEnddate).HasColumnName("carnet_enddate");
            entity.Property(e => e.CarnetId)
                .HasColumnType("int(11)")
                .HasColumnName("carnet_id");
            entity.Property(e => e.CarnetStartdate).HasColumnName("carnet_startdate");
            entity.Property(e => e.UsersKlientId)
                .HasColumnType("int(11)")
                .HasColumnName("users_klient_id");

            entity.HasOne(d => d.Carnet).WithMany(p => p.UsersKlientCarnets)
                .HasForeignKey(d => d.CarnetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_carnets_ibfk_2");

            entity.HasOne(d => d.UsersKlient).WithMany(p => p.UsersKlientCarnets)
                .HasForeignKey(d => d.UsersKlientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_carnets_ibfk_1");
        });

        modelBuilder.Entity<UsersKlientTrening>(entity =>
        {
            entity.HasKey(e => e.TreningId).HasName("PRIMARY");

            entity.ToTable("users_klient_trenings");

            entity.HasIndex(e => e.ExsId, "exercises_id");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.TreningId)
                .HasColumnType("int(11)")
                .HasColumnName("trening_id");
            entity.Property(e => e.ExsId)
                .HasColumnType("int(11)")
                .HasColumnName("exs_id");
            entity.Property(e => e.TreningSeries)
                .HasColumnType("int(11)")
                .HasColumnName("trening_series");
            entity.Property(e => e.TreningWeight)
                .HasColumnType("int(11)")
                .HasColumnName("trening_weight");
            entity.Property(e => e.UsersKlientId)
                .HasColumnType("int(11)")
                .HasColumnName("users_klient_id");
            entity.Property(e => e.UsersKlientTreningDate).HasColumnName("users_klient_trening_date");

            entity.HasOne(d => d.Exs).WithMany(p => p.UsersKlientTrenings)
                .HasForeignKey(d => d.ExsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_trenings_ibfk_2");

            entity.HasOne(d => d.UsersKlient).WithMany(p => p.UsersKlientTrenings)
                .HasForeignKey(d => d.UsersKlientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_trenings_ibfk_1");
        });

        modelBuilder.Entity<UsersKlientTreningplan>(entity =>
        {
            entity.HasKey(e => e.TreningplanId).HasName("PRIMARY");

            entity.ToTable("users_klient_treningplans");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.TreningplanId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("treningplan_id");
            entity.Property(e => e.Friday)
                .HasColumnType("text")
                .HasColumnName("friday");
            entity.Property(e => e.Monday)
                .HasColumnType("text")
                .HasColumnName("monday");
            entity.Property(e => e.Saturday)
                .HasColumnType("text")
                .HasColumnName("saturday");
            entity.Property(e => e.Sunday)
                .HasColumnType("text")
                .HasColumnName("sunday");
            entity.Property(e => e.Thursday)
                .HasColumnType("text")
                .HasColumnName("thursday");
            entity.Property(e => e.Tuesday)
                .HasColumnType("text")
                .HasColumnName("tuesday");
            entity.Property(e => e.UsersKlientId)
                .HasColumnType("int(11)")
                .HasColumnName("users_klient_id");
            entity.Property(e => e.Wendsday)
                .HasColumnType("text")
                .HasColumnName("wendsday");

            entity.HasOne(d => d.UsersKlient).WithMany(p => p.UsersKlientTreningplans)
                .HasForeignKey(d => d.UsersKlientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_klient_treningplans_ibfk_1");
        });

        modelBuilder.Entity<UsersType>(entity =>
        {
            entity.HasKey(e => e.UsersTypeId).HasName("PRIMARY");

            entity.ToTable("users_type");

            entity.Property(e => e.UsersTypeId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("users_type_id");
            entity.Property(e => e.UsersTypeName)
                .HasColumnType("text")
                .HasColumnName("users_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
