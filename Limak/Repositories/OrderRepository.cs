using Limak.Abstractions;
using Limak.Contexts;
using Limak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Repositories
{
    public class OrderRepository : GeneralRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
