using Microsoft.EntityFrameworkCore;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Mappings;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter {
    public class SQSeniorInternalQuotingDbContext : DbContext, ISQSeniorInternalQuotingDbContext {
        public SQSeniorInternalQuotingDbContext(DbContextOptions<SQSeniorInternalQuotingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var mapping = new EntityMapper();
            mapping.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
