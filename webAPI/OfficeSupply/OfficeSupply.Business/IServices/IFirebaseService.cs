using FirebaseAdmin.Auth;
using OfficeSupply.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IFirebaseService
    {
        string VerifyIdToken(AccessToken accessToken);
        UserRecord GetUserInfo(string uid);
        void SendMessage(string regisToken, string title, string body);
    }
}
