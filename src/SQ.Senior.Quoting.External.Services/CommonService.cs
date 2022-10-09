using Microsoft.EntityFrameworkCore;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class CommonService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;

        public CommonService(ISQSeniorExternalQuotingDbContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<State>> GetStates()
            => await _dbContext.Set<State>().ToListAsync();
    }
}