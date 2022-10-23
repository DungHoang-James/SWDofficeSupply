using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class JWTToken
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string AvatarUrl { get; set; }
        public string JwtToken { get; set; }
        //public string RefreshToken { get; set; }
    }
}
