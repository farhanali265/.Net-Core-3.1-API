using Microsoft.EntityFrameworkCore;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter.Mappings {
    internal class EntityMapper {
        public void Configure(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AgentVersionApplicantMap());
        }
    }
}
