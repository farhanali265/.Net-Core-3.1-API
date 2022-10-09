using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class PrescriptionMap : IEntityTypeConfiguration<Prescription> {
        public void Configure(EntityTypeBuilder<Prescription> builder) {

            builder.ToTable("Drugs", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("DrugID");
            builder.Property(c => c.UserID).HasColumnName("UserID");
            builder.Property(c => c.DRXPrescriptionID).HasColumnName("DRXDrugID");
            builder.Property(c => c.PrescriptionName).HasColumnName("DrugName");
            builder.Property(c => c.ChemicalName).HasColumnName("ChemicalName");
            builder.Property(c => c.PrescriptionTypeID).HasColumnName("DrugTypeID");
            builder.Property(c => c.PrescriptionType).HasColumnName("DrugType");
            builder.Property(c => c.PrescriptionTypeNDA).HasColumnName("DrugTypeNDA");
            builder.Property(c => c.AddDate).HasColumnName("AddDate");

            // Relationships
            builder
               .HasMany(r => r.PrescriptionDosages)
               .WithOne()
               .HasForeignKey(dd => dd.PrescriptionID);
        }
    }
}