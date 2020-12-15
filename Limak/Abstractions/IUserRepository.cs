using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Abstractions
{
    public interface IUserRepository : IGeneralRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserWithBalance(string name);
        Task<ApplicationUser> GetUserpackages(string name);
        Task<ApplicationUser> GetUserOrders(string name);
    }
}
