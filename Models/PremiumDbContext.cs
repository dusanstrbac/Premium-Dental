using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PremiumDental.Models
{
    public partial class PremiumDbContext : DbContext
    {
        public PremiumDbContext()
        {
        }

        public PremiumDbContext(DbContextOptions<PremiumDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Doktori> Doktoris { get; set; }
        public virtual DbSet<Karton> Kartons { get; set; }
        public virtual DbSet<KartoniPacijentum> KartoniPacijenta { get; set; }
        public virtual DbSet<Pacijenti> Pacijentis { get; set; }
        public virtual DbSet<Termini> Terminis { get; set; }
        public virtual DbSet<Zakazivanje> Zakazivanjes { get; set; }
      
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DUSAN\\SQLEXPRESS;Initial Catalog=aspnet-premium;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Doktori>(entity =>
            {
                entity.ToTable("Doktori");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Boja).HasMaxLength(50);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Karton>(entity =>
            {
                entity.ToTable("Karton");

                entity.HasIndex(e => e.Id, "IX_Karton")
                    .IsUnique();

                entity.HasIndex(e => e.BrojKartona, "IX_Karton_1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrojKartona)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ZnakUpozorenja)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<KartoniPacijentum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("KartoniPacijenta");

                entity.Property(e => e.BrojKartona).HasMaxLength(50);

                entity.Property(e => e.DatumRodjenja).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ime).IsRequired();

                entity.Property(e => e.Jmbg)
                    .HasMaxLength(50)
                    .HasColumnName("JMBG");

                entity.Property(e => e.LicniDokument).HasMaxLength(50);

                entity.Property(e => e.Pol)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ZnakUpozorenja)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Pacijenti>(entity =>
            {
                entity.ToTable("Pacijenti");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrojKartona).HasMaxLength(50);

                entity.Property(e => e.DatumRodjenja).HasMaxLength(50);

                entity.Property(e => e.Ime).IsRequired();

                entity.Property(e => e.Jmbg)
                    .HasMaxLength(50)
                    .HasColumnName("JMBG");

                entity.Property(e => e.LicniDokument).HasMaxLength(50);

                entity.Property(e => e.Pol)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.ZnakUpozorenja).HasMaxLength(50);
            });

            modelBuilder.Entity<Termini>(entity =>
            {
                entity.ToTable("Termini");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.VremeDo).HasColumnType("datetime");

                entity.Property(e => e.VremeOd).HasColumnType("datetime");
            });

            modelBuilder.Entity<Zakazivanje>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Zakazivanje");

                entity.Property(e => e.Boja).HasMaxLength(50);

                entity.Property(e => e.Expr1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Expr2)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.VremeDo).HasColumnType("datetime");

                entity.Property(e => e.VremeOd).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
