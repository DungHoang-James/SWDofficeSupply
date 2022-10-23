using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;

        public TokenRepository(OfficeSupplyDB officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
        }

        //public void AddToken(Token token)
        //{
        //    _officeSupplyDB.Tokens.Add(token);
        //    _officeSupplyDB.SaveChanges();
        //}

        //public Token GetToken(RefreshToken refreshToken)
        //{
        //    return _officeSupplyDB.Tokens
        //        .FirstOrDefault(t => t.JwtToken.Equals(refreshToken.Token)
        //                        && t.RefreshToken.Equals(refreshToken.Refresh)
        //                        && t.IsUsed == false
        //                        && t.ExpiryDate >= DateTime.UtcNow);
        //}

        //public void UpdateToken(Token token)
        //{
        //    _officeSupplyDB.Tokens.Update(token);
        //    _officeSupplyDB.SaveChanges();
        //}
    }
}
