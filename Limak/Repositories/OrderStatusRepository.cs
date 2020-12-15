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
    public class OrderStatusRepository : GeneralRepository<OrderStatus>, IOrderStatusRepository
    {
        private readonly ApplicationContext context;

        public OrderStatusRepository(ApplicationContext context)
            :base(context)
        {
            this.context = context;
        }

        public async Task<OrderStatus> GetByOrderId(int orderId)
        {
            return await context.OrderStatuses.FirstOrDefaultAsync(os => os.OrderId == orderId);
        }
    }
}
