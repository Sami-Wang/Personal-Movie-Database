using Personal.Movie.Database.Model.UserModel;
using Personal.Movie.Database.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal.Movie.Database.IRepository
{
    public interface IAccountRepository
    {
        Task<List<ValidateUserResult>> Login(LoginInputParameter loginInputParameter);

        Task<List<ValidateUserResult>> Register(RegisterInputParameter registerInputParameter);
    }
}
