using Microsoft.EntityFrameworkCore;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    internal class EntityMapper {
        public void Configure(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AgentVersionApplicantPrescriptionMap());
            modelBuilder.ApplyConfiguration(new AnthemContractMap());
            modelBuilder.ApplyConfiguration(new ApplicantPrescriptionMap());
            modelBuilder.ApplyConfiguration(new ApplicantProviderMap());
            modelBuilder.ApplyConfiguration(new ApplicantTrackingMap());
            modelBuilder.ApplyConfiguration(new AppLogMap());
            modelBuilder.ApplyConfiguration(new AspNetUserMap());
            modelBuilder.ApplyConfiguration(new PrescriptionDosageMap());
            modelBuilder.ApplyConfiguration(new PrescriptionMap());
            modelBuilder.ApplyConfiguration(new DrxEnrollmentLogMap());
            modelBuilder.ApplyConfiguration(new NetworkFipMap());
            modelBuilder.ApplyConfiguration(new PharmacyMap());
            modelBuilder.ApplyConfiguration(new StateMap());
            modelBuilder.ApplyConfiguration(new UserPlanMap());
            modelBuilder.ApplyConfiguration(new ZelisErrorMap());
            modelBuilder.ApplyConfiguration(new ZelisLogMap());
            modelBuilder.ApplyConfiguration(new ZelisNetworkMap());
            modelBuilder.ApplyConfiguration(new ZelisProvider_Location_Network_ProviderType_SpecialtyMap());
            modelBuilder.ApplyConfiguration(new ZelisProvider_Location_Network_ProviderTypeMap());
            modelBuilder.ApplyConfiguration(new ZelisProvider_Location_NetworkMap());
            modelBuilder.ApplyConfiguration(new ZelisProvider_LocationMap());
            modelBuilder.ApplyConfiguration(new ZelisProviderMap());
            modelBuilder.ApplyConfiguration(new ZelisProviderTypeMap());
            modelBuilder.ApplyConfiguration(new ZelisSpecialtyMap());
            modelBuilder.ApplyConfiguration(new ZipFipsMappingMap());
        }
    }
}