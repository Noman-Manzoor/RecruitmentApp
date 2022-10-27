using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Entities
{
    public partial class DMRecruitmentContext : DbContext
    {
        public DMRecruitmentContext()
        {
        }

        public DMRecruitmentContext(DbContextOptions<DMRecruitmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<FamilyDetail> FamilyDetails { get; set; } = null!;
        public virtual DbSet<LeadManagement> LeadManagements { get; set; } = null!;
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; } = null!;
        public virtual DbSet<ProfileManagement> ProfileManagements { get; set; } = null!;
        public virtual DbSet<Recruiter> Recruiters { get; set; } = null!;
        public virtual DbSet<RecruitmentCompany> RecruitmentCompanies { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SettingType> SettingTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QFENKPQ\\SQLEXPRESS;Database=DMRecruitment;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");
            });

            modelBuilder.Entity<FamilyDetail>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.ToTable("FamilyDetail");

                entity.Property(e => e.ProfileId).ValueGeneratedNever().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

                entity.Property(e => e.Id).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.HasOne(d => d.Profile)
                    .WithOne(p => p.FamilyDetail)
                    .HasForeignKey<FamilyDetail>(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyDetail_Profile");
            });

            modelBuilder.Entity<LeadManagement>(entity =>
            {
                entity.ToTable("LeadManagement");

                entity.HasOne(d => d.RecruitmentCompany)
                    .WithMany(p => p.LeadManagements)
                    .HasForeignKey(d => d.RecruiterCompanyId)
                    .HasConstraintName("FK_LeadManagements_RecruitmentCompany");


                entity.HasOne(d => d.ProfileManagement)
                    .WithMany(p => p.LeadManagements)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_LeadManagements_ProfileManagement");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.LeadManagements)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_LeadManagements_Company");



                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HourlyRate).HasColumnType("money");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<ProfileManagement>(entity =>
            {
                entity.ToTable("ProfileManagement");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Recruiter>(entity =>
            {
                entity.ToTable("Recruiter");

                entity.HasOne(d => d.RecruitmentCompany)
                    .WithMany(p => p.Recruiters)
                    .HasForeignKey(d => d.RecruitmentCompanyId)
                    .HasConstraintName("FK_Recruiter_RecruitmentCompany");
            });

            modelBuilder.Entity<PersonalInformation>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.ToTable("PersonalInformation");

                entity.Property(x => x.IsDeleted);

                entity.HasOne(d => d.RecruitmentCompany)
                    .WithMany(p => p.PersonalInformation)
                    .HasForeignKey(d => d.RecruitmentCompanyId)
                    .HasConstraintName("FK_PersonalInformation_RecruitmentCompany");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PersonalInformation)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_PersonalInformation_Company");

                entity.HasOne(d => d.ProfileManagement)
                    .WithMany(p => p.PersonalInformation)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_ProfileManagment_Company");
            });

            modelBuilder.Entity<RecruitmentCompany>(entity =>
            {
                entity.ToTable("RecruitmentCompany");

                entity.Property(e => e.CreatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.SettingId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SettingType>(entity =>
            {
                entity.ToTable("SettingType");

                entity.Property(e => e.SettingTypeId).ValueGeneratedNever();

                entity.Property(e => e.SettingType1).HasColumnName("SettingType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
