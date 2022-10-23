using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        //string GenerateRefreshToken();
        //void AddToken(int userID, string jwtToken, string refreshToken);
        //Token CheckRefreshToken(RefreshToken refreshToken);
        //void ExpiredToken(Token token);
    }
}
