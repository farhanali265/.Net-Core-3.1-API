using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ApplicantPrescriptionMap : IEntityTypeConfiguration<ApplicantPrescription> {
        public void Configure(EntityTypeBuilder<ApplicantPrescription> builder) {

            builder.ToTable("ApplicantPrescription", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("PKID");
            builder.Property(c => c.ApplicantID).HasColumnName("ApplicantID");
            builder.Property(c => c.PrescriptionID).HasColumnName("DrugID");
            builder.Property(c => c.AmountPer30Days).HasColumnName("AmountPer30Days");
            builder.Property(c => c.DosageID).HasColumnName("DosageID");
            builder.Property(c => c.Package).HasColumnName("Package");
            builder.Property(c => c.Dated).HasColumnName("Dated");
            builder.Property(c => c.SelectedPrescriptionInfoAsXml).HasColumnName("SelectedDrugInfoAsXml");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.Property(c => c.PrescriptionName).HasColumnName("DrugName");

            // Relationships
        }
    }
}