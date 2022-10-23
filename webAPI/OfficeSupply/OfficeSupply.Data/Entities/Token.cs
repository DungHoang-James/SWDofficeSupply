using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Token))]
    public class Token
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    }
}
