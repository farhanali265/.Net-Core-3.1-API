using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter.Mappings {
    internal class AgentVersionApplicantMap : IEntityTypeConfiguration<AgentVersionApplicant> {
        public void Configure(EntityTypeBuilder<AgentVersionApplicant> builder) {
            builder.ToTable("AgentVersionApplicant", "dbo");
            builder.HasKey(c => c.ApplicantId);
        }
    }
}
