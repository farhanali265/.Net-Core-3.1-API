using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class UserPlanMap : IEntityTypeConfiguration<UserPlan> {
        public void Configure(EntityTypeBuilder<UserPlan> builder) {

            builder.ToTable("UserPlans", "dbo");
            builder.HasKey(c => c.UserPlainID);

            // Column Mappings
            builder.Property(c => c.UserPlainID).HasColumnName("UserPlainID");
            builder.Property(c => c.UserID).HasColumnName("UserID");
            builder.Property(c => c.CarrierName).HasColumnName("CarrierName");
            builder.Property(c => c.ContractID).HasColumnName("ContractID");
            builder.Property(c => c.PrescriptionDeductible).HasColumnName("DrugDeductible");
            builder.Property(c => c.PrescriptionPremium).HasColumnName("DrugPremium");
            builder.Property(c => c.EstimatedAnnualPrescriptionCost).HasColumnName("EstimatedAnnualDrugCost");
            builder.Property(c => c.EstimatedAnnualMedicalCost).HasColumnName("EstimatedAnnualMedicalCost");
            builder.Property(c => c.ID).HasColumnName("ID");
            builder.Property(c => c.MaximumOutOfPocketCost).HasColumnName("MaximumOutOfPocketCost");
            builder.Property(c => c.MedicalDeductible).HasColumnName("MedicalDeductible");
            builder.Property(c => c.MedicalPremium).HasColumnName("MedicalPremium");
            builder.Property(c => c.PlanID).HasColumnName("PlanID");
            builder.Property(c => c.PlanName).HasColumnName("PlanName");
            builder.Property(c => c.PlanRating).HasColumnName("PlanRating");
            builder.Property(c => c.PlanSubType).HasColumnName("PlanSubType");
            builder.Property(c => c.PlanType).HasColumnName("PlanType");
            builder.Property(c => c.PlanYear).HasColumnName("PlanYear");
            builder.Property(c => c.SegmentID).HasColumnName("SegmentID");
            builder.Property(c => c.AddDate).HasColumnName("AddDate");
            builder.Property(c => c.LogoName).HasColumnName("LogoName");

            // Relationships
        }
    }
}