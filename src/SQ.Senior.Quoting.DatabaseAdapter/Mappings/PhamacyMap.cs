using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class PharmacyMap : IEntityTypeConfiguration<Pharmacy> {
        public void Configure(EntityTypeBuilder<Pharmacy> builder) {

            builder.ToTable("Pharmacies", "dbo");
            builder.HasKey(c => c.Id);

            // Column Mappings
            builder.Property(c => c.Id).HasColumnName("PharmacyID");
            builder.Property(c => c.PharmacyDrxId).HasColumnName("PharmacyDRXID");
            builder.Property(c => c.PharmacyName).HasColumnName("PharmacyName");
            builder.Property(c => c.Address1).HasColumnName("Address1");
            builder.Property(c => c.Address2).HasColumnName("Address2");
            builder.Property(c => c.City).HasColumnName("City");
            builder.Property(c => c.ZipCode).HasColumnName("ZipCode");
            builder.Property(c => c.StateCode).HasColumnName("StateCode");
            builder.Property(c => c.PharmacyPhone).HasColumnName("PharmacyPhone");
            builder.Property(c => c.Latitude).HasColumnName("Latitude");
            builder.Property(c => c.Longitude).HasColumnName("Longitude");
            builder.Property(c => c.Distance).HasColumnName("Distance");
            builder.Property(c => c.PharmacyNpi).HasColumnName("PharmacyNPI");
            builder.Property(c => c.Has24hrService).HasColumnName("Has24hrService");
            builder.Property(c => c.HasCompounding).HasColumnName("HasCompounding");
            builder.Property(c => c.HasDelivery).HasColumnName("HasDelivery");
            builder.Property(c => c.HasDriveup).HasColumnName("HasDriveup");
            builder.Property(c => c.HasDurableEquipment).HasColumnName("HasDurableEquipment");
            builder.Property(c => c.HasEPrescriptions).HasColumnName("HasEPrescriptions");
            builder.Property(c => c.HasHandicapAccess).HasColumnName("HasHandicapAccess");
            builder.Property(c => c.IsHomeInfusion).HasColumnName("IsHomeInfusion");
            builder.Property(c => c.IsLongTermCare).HasColumnName("IsLongTermCare");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.Property(c => c.Dated).HasColumnName("Dated");
            builder.Property(c => c.IsDeleted).HasColumnName("isDeleted");
            builder.Property(c => c.JsonResponse).HasColumnName("JsonResponse");
            builder.Property(c => c.QrsPharmacyId).HasColumnName("QrsPharmacyId");

            // Relationships
        }
    }
}