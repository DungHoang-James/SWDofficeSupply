using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.PropertyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class BaseRepository<Table> : IBaseRepository<Table> where Table : class
    {
        private DbSet<Table> dbSet;
        private readonly OfficeSupplyDB _officeSupplyDB;

        public BaseRepository(OfficeSupplyDB officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this.dbSet = _officeSupplyDB.Set<Table>();
        }

        public virtual Table GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<Table> GetMany(Expression<Func<Table,bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
        public virtual bool Create(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }
            try
            {
                dbSet.Add(table);
                _officeSupplyDB.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public virtual bool Update(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            try
            {
                dbSet.Attach(table);
                _officeSupplyDB.Update<Table>(table);
                _officeSupplyDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
