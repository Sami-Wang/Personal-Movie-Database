using System.Collections.Generic;

namespace Personal.Movie.Database.Models.Account
{
    public class AccountUser
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public virtual ICollection<AccountMapUserRole> Roles { get; private set; } 
            = new List<AccountMapUserRole>();
    }
}
