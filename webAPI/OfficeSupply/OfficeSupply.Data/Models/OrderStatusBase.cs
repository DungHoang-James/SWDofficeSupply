using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models
{
    public class OrderStatusBase
    {
        public static int WAITING = 1;
        public static int LEADER_APPROVE = 2;
        public static int MANAGER_APPROVE = 3;
        public static int CANCEL = 4;
    }
}
