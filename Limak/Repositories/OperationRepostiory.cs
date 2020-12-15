using Limak.Abstractions;
using Limak.Contexts;
using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Repositories
{
    public class OperationRepostiory : GeneralRepository<Operation>, IOperationRepository
    {
        public OperationRepostiory(ApplicationContext context)
            : base(context)
        {
        }
    }
}
