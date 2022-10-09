using Microsoft.EntityFrameworkCore;
using SQ.Senior.Quoting.Internal.DatabaseAdapter.Abstractions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter.Repositories {
    public class SeniorInternalQuotingDbRepository : ISeniorInternalQuotingDbRepository {
        private readonly ISQSeniorInternalQuotingDbContext _internalQuotingDbContext;

        public SeniorInternalQuotingDbRepository(ISQSeniorInternalQuotingDbContext internalQuotingDbContext) {
            _internalQuotingDbContext = internalQuotingDbContext ?? throw new ArgumentNullException(nameof(internalQuotingDbContext));
        }

        public async Task<TEntity> FindSingleByAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
            => await _internalQuotingDbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }
}
