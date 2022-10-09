using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.Internal.DatabaseAdapter.Repositories {
    public interface ISeniorInternalQuotingDbRepository {
        Task<TEntity> FindSingleByAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
