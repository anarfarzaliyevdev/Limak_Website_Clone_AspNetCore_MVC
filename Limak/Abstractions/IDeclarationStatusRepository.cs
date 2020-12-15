using Limak.Models;
using Limak.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Abstractions
{
    public interface IDeclarationStatusRepository : IGeneralRepository<DeclarationStatus>
    {
        Task<DeclarationStatus> GetByDeclarationId(int declarationId);
    }
}
