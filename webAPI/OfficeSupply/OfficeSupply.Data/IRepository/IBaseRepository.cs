using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IBaseRepository<Table> where Table : class
    {
        Table GetById(int id);
        bool Create(Table table);
        bool Update(Table table);
    }
}
