using Limak.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Contexts
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<DeclarationStatus> DeclarationStatuses { get; set; }
        
    }
}
