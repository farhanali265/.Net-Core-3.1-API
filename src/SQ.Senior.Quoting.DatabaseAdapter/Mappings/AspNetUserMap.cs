using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class AspNetUserMap : IEntityTypeConfiguration<AspNetUser> {
        public void Configure(EntityTypeBuilder<AspNetUser> builder) {

            builder.ToTable("AspNetUsers", "dbo");
            builder.HasKey(c => c.Id);

            // Column Mappings
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Email).HasColumnName("Email");
            builder.Property(c => c.EmailConfirmed).HasColumnName("EmailConfirmed");
            builder.Property(c => c.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(c => c.SecurityStamp).HasColumnName("SecurityStamp");
            builder.Property(c => c.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            builder.Property(c => c.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            builder.Property(c => c.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            builder.Property(c => c.LockoutEnabled).HasColumnName("LockoutEnabled");
            builder.Property(c => c.AccessFailedCount).HasColumnName("AccessFailedCount");
            builder.Property(c => c.UserName).HasColumnName("UserName");
            builder.Property(c => c.FirstName).HasColumnName("FirstName");
            builder.Property(c => c.MiddleInitial).HasColumnName("MiddleInitial");
            builder.Property(c => c.LastName).HasColumnName("LastName");
            builder.Property(c => c.Phone).HasColumnName("Phone");
            builder.Property(c => c.DateOfBirth).HasColumnName("DateOfBirth");
            builder.Property(c => c.ZipCode).HasColumnName("ZIPCode");
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(c => c.FipsCode).HasColumnName("FipsCode");
            builder.Property(c => c.ExtraHelpLevel).HasColumnName("ExtraHelpLevel");
            builder.Property(c => c.QrsApplicantId).HasColumnName("QrsApplicantId");
            builder.Property(c => c.QrsContactId).HasColumnName("QrsContactId");
            builder.Property(c => c.EntrySource).HasColumnName("EntrySource");
            builder.Property(c => c.EntryToken).HasColumnName("EntryToken");
            builder.Property(c => c.EntryKeyword).HasColumnName("EntryKeyword");
            builder.Property(c => c.EntryPhoneNumber).HasColumnName("EntryPhoneNumber");
            builder.Property(c => c.PrescriptionDoesNotApply).HasColumnName("PrescriptionDoesNotApply");
            builder.Property(c => c.ProviderDoesNotApply).HasColumnName("ProviderDoesNotApply");
            builder.Property(c => c.PharmacyDoesNotApply).HasColumnName("PharmacyDoesNotApply");
            builder.Property(c => c.AccountId).HasColumnName("AccountId");
            builder.Property(c => c.IndividualId).HasColumnName("IndividualId");
            builder.Property(c => c.Phone).HasColumnName("PhoneNumber");

            // Relationships
        }
    }
}