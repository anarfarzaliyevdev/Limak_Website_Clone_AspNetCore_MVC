using Limak.Abstractions;
using Limak.Contexts;
using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Repositories
{
    public class DeclarationRepository : GeneralRepository<Declaration>, IDeclarationRepository
    {
        public DeclarationRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
