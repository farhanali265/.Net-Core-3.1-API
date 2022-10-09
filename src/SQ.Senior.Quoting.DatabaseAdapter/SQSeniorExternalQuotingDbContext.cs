using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Mappings;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.DatabaseAdapter {
    public class SQSeniorExternalQuotingDbContext : DbContext, ISQSeniorExternalQuotingDbContext {
        private readonly ILogger<SQSeniorExternalQuotingDbContext> _logger;

        public SQSeniorExternalQuotingDbContext(DbContextOptions<SQSeniorExternalQuotingDbContext> options,
                                                ILogger<SQSeniorExternalQuotingDbContext> logger) : base(options) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var mapping = new EntityMapper();
            mapping.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            var validationErrors = ChangeTracker
                .Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success);

            if (validationErrors.Any()) {
                // TODO: Possibly throw an exception here
                foreach (var eve in validationErrors) {
                    foreach (var ent in eve.MemberNames) {
                        _logger.LogError($"Entity of type {ent} has the following validation error: {eve.ErrorMessage}");
                    }
                }
            }

            return base.SaveChangesAsync();
        }
    }
}