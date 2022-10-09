using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class DrxEnrollmentLogMap : IEntityTypeConfiguration<DrxEnrollmentLog> {
        public void Configure(EntityTypeBuilder<DrxEnrollmentLog> builder) {

            builder.ToTable("DrxEnrollmentLog", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ID");
            builder.Property(c => c.SQSAccountId).HasColumnName("SQSAccountId");
            builder.Property(c => c.SQSApplicantId).HasColumnName("SQSApplicantId");
            builder.Property(c => c.UserID).HasColumnName("UserID");
            builder.Property(c => c.PlanName).HasColumnName("PlanName");
            builder.Property(c => c.Provider).HasColumnName("Provider");
            builder.Property(c => c.ContractId).HasColumnName("ContractId");
            builder.Property(c => c.PbpId).HasColumnName("PbpId");
            builder.Property(c => c.SegmentId).HasColumnName("SegmentId");
            builder.Property(c => c.DrxPlanId).HasColumnName("DrxPlanId");
            builder.Property(c => c.DrxSessionId).HasColumnName("DrxSessionId");
            builder.Property(c => c.EmfluenceEmailLogId).HasColumnName("EmfluenceEmailLogId");
            builder.Property(c => c.RequestXml).HasColumnName("RequestXml");
            builder.Property(c => c.ResponseXml).HasColumnName("ResponseXml");
            builder.Property(c => c.EnrollmentError).HasColumnName("EnrollmentError");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");
            builder.Property(c => c.PhysicianId).HasColumnName("PhysicianId");
            builder.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber");

            // Relationships
        }
    }
}