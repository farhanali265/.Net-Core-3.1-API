using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class AgentVersionApplicantPrescriptionMap : IEntityTypeConfiguration<AgentVersionApplicantPrescription> {
        public void Configure(EntityTypeBuilder<AgentVersionApplicantPrescription> builder) {

            builder.ToTable("AgentVersionApplicantPrescription", "dbo");
            builder.HasKey(c => c.Id);

            // Column Mappings
            builder.Property(c => c.Id).HasColumnName("PKID");

            builder.Property(c => c.ApplicantId).HasColumnName("ApplicantID");

            builder.Property(c => c.PrescriptionId).HasColumnName("DrugID");

            builder.Property(c => c.AmountPerThirtyDays).HasColumnName("AmountPer30Days");

            builder.Property(c => c.DosageId).HasColumnName("DosageID");

            builder.Property(c => c.Package).HasColumnName("Package");

            builder.Property(c => c.Dated).HasColumnName("Dated");

            builder.Property(c => c.SelectedPrescriptionInfoAsXml).HasColumnName("SelectedDrugInfoAsXml");

            builder.Property(c => c.SqsApplicantId).HasColumnName("SQSApplicantID");

            builder.Property(c => c.NationalDrugCode).HasColumnName("NDC");

            builder.Property(c => c.PrescriptionName).HasColumnName("DrugName");

            builder.Property(c => c.UserId).HasColumnName("UserId");

            builder.Property(c => c.PrescriptionLabel).HasColumnName("Drug");

            builder.Property(c => c.PackageName).HasColumnName("PackageName");

            builder.Property(c => c.QrsAgentPrescriptionId).HasColumnName("QrsAgentPrescriptionId");

            // Relationships
        }
    }
}