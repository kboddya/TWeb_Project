using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopProject.Domain.Enums.Order
{
    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Delivered = 2,
        Cancelled = 3
    }
}
