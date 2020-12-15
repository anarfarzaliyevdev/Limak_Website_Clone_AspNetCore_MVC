using Limak.Abstractions;
using Limak.Contexts;
using Limak.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Repositories
{
    public class DeclarationStatusRepository : GeneralRepository<DeclarationStatus>, IDeclarationStatusRepository
    {
        private readonly ApplicationContext context;

        public DeclarationStatusRepository(ApplicationContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<DeclarationStatus> GetByDeclarationId(int declarationId)
        {
            return await context.DeclarationStatuses.FirstOrDefaultAsync(ds => ds.DeclarationId == declarationId);
        }
    }
}
