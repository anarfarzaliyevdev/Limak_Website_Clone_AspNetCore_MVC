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
    public class UserRepository : GeneralRepository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<ApplicationUser> GetUserWithBalance(string name)
        {
            return await context.Users.Include("Balance").FirstOrDefaultAsync(u=>u.UserName==name);
        }
        public async Task<ApplicationUser> GetUserpackages(string name)
        {
            return await context.Users.Include("Declarations").FirstOrDefaultAsync(u => u.UserName == name);
        }
        public async Task<ApplicationUser> GetUserOrders(string name)
        {
            return await context.Users.Include("Orders").FirstOrDefaultAsync(u => u.UserName == name);
        }
    }
}
