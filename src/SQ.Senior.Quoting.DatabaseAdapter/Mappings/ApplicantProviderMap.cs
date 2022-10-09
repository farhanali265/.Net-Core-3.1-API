using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ApplicantProviderMap : IEntityTypeConfiguration<ApplicantProvider> {
        public void Configure(EntityTypeBuilder<ApplicantProvider> builder) {

            builder.ToTable("ApplicantProvider", "dbo");
            builder.HasKey(c => c.Id);

            // Column Mappings
            builder.Property(c => c.Id).HasColumnName("ID");
            builder.Property(c => c.SQSAccountId).HasColumnName("SQSAccountId");
            builder.Property(c => c.SQSApplicantId).HasColumnName("SQSApplicantId");
            builder.Property(c => c.ProviderType).HasColumnName("ProviderType");
            builder.Property(c => c.NationalProviderIdentifier).HasColumnName("NpiNumber");
            builder.Property(c => c.Name).HasColumnName("Name");
            builder.Property(c => c.Address1).HasColumnName("Address1");
            builder.Property(c => c.Address2).HasColumnName("Address2");
            builder.Property(c => c.City).HasColumnName("City");
            builder.Property(c => c.State).HasColumnName("State");
            builder.Property(c => c.ZipCode).HasColumnName("Zip");
            builder.Property(c => c.Phone).HasColumnName("Phone");
            builder.Property(c => c.IsActive).HasColumnName("Active");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.Property(c => c.Specialty).HasColumnName("Specialty");
            builder.Property(c => c.QrsProviderId).HasColumnName("QrsProviderId");

            // Relationships
        }
    }
}