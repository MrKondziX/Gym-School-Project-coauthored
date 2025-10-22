using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Projekt_Siłownia;

public partial class GymAppDbContext : DbContext
{
    public GymAppDbContext()
    {
    }

    public GymAppDbContext(DbContextOptions<GymAppDbContext> options)
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

    public virtual DbSet<UsersKlientTreningday> UsersKlientTreningdays { get; set; }

    public virtual DbSet<UsersKlientTreningplan> UsersKlientTreningplans { get; set; }

    public virtual DbSet<UsersType> UsersTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=gym-app-db.c3w0aye8krgk.eu-north-1.rds.amazonaws.com;database=gym-app-db;user=GymAppAcc;password=P@p!ez.2137$", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carnet>(entity =>
        {
            entity.HasKey(e => e.CarnetId).HasName("PRIMARY");

            entity
                .ToTable("carnets")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.CarnetId).HasColumnName("carnet_id");
            entity.Property(e => e.CarnetCost).HasColumnName("carnet_cost");
            entity.Property(e => e.CarnetName)
                .HasColumnType("text")
                .HasColumnName("carnet_name");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExsId).HasName("PRIMARY");

            entity
                .ToTable("exercises")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.ExsMuscleId, "exercises_muscle_id");

            entity.Property(e => e.ExsId).HasColumnName("exs_id");
            entity.Property(e => e.ExsDescription)
                .HasColumnType("text")
                .HasColumnName("exs_description");
            entity.Property(e => e.ExsMuscleId).HasColumnName("exs_muscle_id");
            entity.Property(e => e.ExsName)
                .HasColumnType("text")
                .HasColumnName("exs_name");
        });

        modelBuilder.Entity<ExercisesMuscle>(entity =>
        {
            entity.HasKey(e => e.ExsMuscleId).HasName("PRIMARY");

            entity
                .ToTable("exercises_muscles")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.ExsMuscleId).HasColumnName("exs_muscle_id");
            entity.Property(e => e.ExsMuscleName)
                .HasColumnType("text")
                .HasColumnName("exs_muscle_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.UsersTypeId, "users_type_id");

            entity.Property(e => e.UsersId)
                .ValueGeneratedNever()
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
            entity.Property(e => e.UsersTypeId).HasColumnName("users_type_id");
        });

        modelBuilder.Entity<UsersCoach>(entity =>
        {
            entity.HasKey(e => e.UsersCoachId).HasName("PRIMARY");

            entity
                .ToTable("users_coach")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.UsersId, "users_id");

            entity.Property(e => e.UsersCoachId)
                .ValueGeneratedNever()
                .HasColumnName("users_coach_id");
            entity.Property(e => e.UsersCoachNott)
                .HasColumnType("text")
                .HasColumnName("users_coach_nott");
            entity.Property(e => e.UsersId).HasColumnName("users_id");
        });

        modelBuilder.Entity<UsersKlient>(entity =>
        {
            entity.HasKey(e => e.UsersKlientId).HasName("PRIMARY");

            entity
                .ToTable("users_klient")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.UsersCoachId, "connect_coach");

            entity.HasIndex(e => e.UsersId, "users_id");

            entity.Property(e => e.UsersKlientId)
                .ValueGeneratedNever()
                .HasColumnName("users_klient_id");
            entity.Property(e => e.UsersCoachId).HasColumnName("users_coach_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");
        });

        modelBuilder.Entity<UsersKlientCarnet>(entity =>
        {
            entity.HasKey(e => e.UsersKlientCarnetId).HasName("PRIMARY");

            entity
                .ToTable("users_klient_carnets")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.CarnetId, "carnet_id");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.UsersKlientCarnetId)
                .ValueGeneratedNever()
                .HasColumnName("users_klient_carnet_id");
            entity.Property(e => e.CarnetEnddate).HasColumnName("carnet_enddate");
            entity.Property(e => e.CarnetId).HasColumnName("carnet_id");
            entity.Property(e => e.CarnetStartdate).HasColumnName("carnet_startdate");
            entity.Property(e => e.UsersKlientId).HasColumnName("users_klient_id");
        });

        modelBuilder.Entity<UsersKlientTrening>(entity =>
        {
            entity.HasKey(e => e.TreningId).HasName("PRIMARY");

            entity
                .ToTable("users_klient_trenings")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.ExsId, "exercises_id");

            entity.HasIndex(e => e.UsersTreningdayId, "treningday");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.TreningId)
                .ValueGeneratedNever()
                .HasColumnName("trening_id");
            entity.Property(e => e.ExsId).HasColumnName("exs_id");
            entity.Property(e => e.TreningSeries).HasColumnName("trening_series");
            entity.Property(e => e.TreningWeight).HasColumnName("trening_weight");
            entity.Property(e => e.UsersKlientId).HasColumnName("users_klient_id");
            entity.Property(e => e.UsersKlientTreningDate).HasColumnName("users_klient_trening_date");
            entity.Property(e => e.UsersTreningdayId).HasColumnName("users_treningday_id");
        });

        modelBuilder.Entity<UsersKlientTreningday>(entity =>
        {
            entity.HasKey(e => e.UsersTreningdayId).HasName("PRIMARY");

            entity
                .ToTable("users_klient_treningday")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.UsersKlientId, "users_klient");

            entity.Property(e => e.UsersTreningdayId)
                .ValueGeneratedNever()
                .HasColumnName("users_treningday_id");
            entity.Property(e => e.UsersKlientId).HasColumnName("users_klient_id");
            entity.Property(e => e.UsersTreningdayDate).HasColumnName("users_treningday_date");
            entity.Property(e => e.UsersTreningdayTime)
                .HasMaxLength(32)
                .HasColumnName("users_treningday_time");
        });

        modelBuilder.Entity<UsersKlientTreningplan>(entity =>
        {
            entity.HasKey(e => e.TreningplanId).HasName("PRIMARY");

            entity
                .ToTable("users_klient_treningplans")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.ExsId, "exercise");

            entity.HasIndex(e => e.UsersKlientId, "users_klient_id");

            entity.Property(e => e.TreningplanId)
                .ValueGeneratedNever()
                .HasColumnName("treningplan_id");
            entity.Property(e => e.ExsId).HasColumnName("exs_id");
            entity.Property(e => e.TreningplanDate).HasColumnName("treningplan_date");
            entity.Property(e => e.TreningplanNote)
                .HasColumnType("text")
                .HasColumnName("treningplan_note");
            entity.Property(e => e.UsersKlientId).HasColumnName("users_klient_id");
        });

        modelBuilder.Entity<UsersType>(entity =>
        {
            entity.HasKey(e => e.UsersTypeId).HasName("PRIMARY");

            entity
                .ToTable("users_type")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.UsersTypeId)
                .ValueGeneratedNever()
                .HasColumnName("users_type_id");
            entity.Property(e => e.UsersTypeName)
                .HasColumnType("text")
                .HasColumnName("users_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
